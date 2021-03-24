using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeLinkUp.Data;
using WeLinkUp.Models;

namespace WeLinkUp.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _securityManager;

        public CalendarController(ApplicationDbContext context, UserManager<ApplicationUser> secMgr)
        {
            this._context = context;
            this._securityManager = secMgr;
        }
        [HttpGet]
        public IActionResult Calendar()
        {
            return View();
        }

        [HttpGet]
        [Route("api/Events")]
        public async Task<IEnumerable<ViewCalendarModel>> GetEventsAsync([FromQuery] DateTime start, [FromQuery] DateTime end)
        {

            // get current user to get its id(host)
            var user = await _securityManager.GetUserAsync(User);

            // populate schedule on calendar
            var schedule = (from c in _context.Calendar
                            join e in _context.Events on c.EventId equals e.EventId
                            where c.UserId == user.Id 
                            select new ViewCalendarModel
                            {
                                Id = c.CalendarId,
                                Start = DateTime.Parse(e.Date + " " + e.StartTime),
                                End = DateTime.Parse(e.Date + " " + e.EndTime),
                                Text = e.EventTitle,
                                Color = e.EventType == 0 ? "#A2C4C9": e.HostId == user.Id ? "#EA9999" : "#FFE599",
                                Resource = e.EventId
                            }).ToList().Where(a => !((a.End <= start) || (a.Start >= end)));
                            // Personal event: blue / Attending event: yellow / Event I'm hosting: red
          
            return schedule;
            
        }

        // DELETE: api/Events/id
        [HttpDelete("api/Events/{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // get current user to get its id(host)
            var user = await _securityManager.GetUserAsync(User);

            // 1. get event id 
            var @event = (from c in _context.Calendar
                            join e in _context.Events on c.EventId equals e.EventId
                            where c.CalendarId == id
                            select new
                            {
                              EventId = e.EventId,
                              HostId = e.HostId,
                              EventTitle = e.EventTitle
                            }).FirstOrDefault();

            if (@event ==null) // if event is not found
            {
                return NotFound();
            }
            // 2. check if user is the host
            if (@event.HostId == user.Id) // 3. if host -> remove from calendar for everyone, delete event, send notification, etc
            {
                // remove event from everyone's calendar
                var calendar = _context.Calendar.Where(c => c.EventId == @event.EventId);
                foreach (var c in calendar)
                {
                    _context.Calendar.Remove(c);
                }
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine("calendar removed" + @event.EventId);
                // remove attendee
                var attendee = _context.AttendeeList.Where(a => a.EventId == @event.EventId);
                foreach (var a in attendee)
                {
                    _context.AttendeeList.Remove(a);
                }
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine("attendee removed" + @event.EventId);
                // remove invitation
                var notification = _context.Notifications.Where(n => n.EventId == @event.EventId);
                foreach (var n in notification)
                {
                    _context.Notifications.Remove(n);
                }
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine("invitation removed" + @event.EventId);
                // delete event
                var eventToDelete = (from e in _context.Events
                                     where e.EventId == @event.EventId
                                     select new CreateEvent
                                     {
                                         EventId = e.EventId
                                     }).FirstOrDefault();


                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine("event removed" + @event.EventId);
                // send cancellation notification
                var query_getFriends_notification = (from f in _context.FriendLists
                                                     join u in _context.Users on f.FriendId equals u.Id
                                                     where f.UserId == user.Id
                                                     select new Notification
                                                     {
                                                         RecipientId = f.FriendId,
                                                         SenderId = f.UserId,
                                                         Message = "Event [" + @event.EventTitle + "] by ["+ user.UserName +"] has been cancelled.",
                                                         NotificationDate = DateTime.Now.ToString()
                                                     }); 
                // convert to List<Notification>
                List<Notification> notifications = new List<Notification>(query_getFriends_notification);
                // Add to Notification
                foreach (Notification n in notifications)
                {
                    _context.Notifications.Add(n);
                }
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine("invitation sent" + @event.EventId);
            }
            else // 4. if attendee -> remove from calendar, change attendee status
            {
                // remove event from user's calendar
                var calendar = _context.Calendar.Where(c => c.EventId == @event.EventId && c.UserId == user.Id).FirstOrDefault();                
                _context.Calendar.Remove(calendar);
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine("calendar removed" + @event.EventId);

                // update invitation status
                var attendee = _context.AttendeeList.Where(a => a.EventId == @event.EventId && a.UserId == user.Id).FirstOrDefault();
                attendee.Status = "Invited";
                _context.AttendeeList.Update(attendee);
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine("status updated" + @event.EventId);
            }
            

            return Ok(@event);


        }

    }
}
