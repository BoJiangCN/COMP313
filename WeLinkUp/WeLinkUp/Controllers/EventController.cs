using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using WeLinkUp.Data;
using WeLinkUp.Models;

namespace WeLinkUp.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _securityManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> secMgr)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;
            this._securityManager = secMgr;


        }
        [HttpGet]

        public IActionResult CreateEvent()
        {
            return View();
        }
        
         

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync(CreateEvent e, bool isAdultOnlyChecked)
        {

            if (ModelState.IsValid)
            {
                //code for add imgae to db
                string wwwroot = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(e.Image);
                string extension = Path.GetExtension(e.Image);
                e.Image = fileName = fileName + extension;
                string path = Path.Combine(wwwroot + "/images/EventPicture/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {


                }

                // get current user to get its id(host)
                var user = await _securityManager.GetUserAsync(User);


                if (isAdultOnlyChecked == true)
                {
                    e.IsAdultOnly = 1; //true
                }
                else
                {
                    e.IsAdultOnly = 0; //false
                }
                //Create new event instance
                var newEvent = new CreateEvent
                {
                    EventTitle = e.EventTitle,
                    Location = e.Location,
                    Date = e.Date,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Description = e.Description,
                    Image = e.Image,
                    IsAdultOnly = e.IsAdultOnly,
                    EventType = e.EventType,
                    HostId = user.Id

                };

                //save event in database
                _context.Events.Add(newEvent);
                _context.SaveChanges();

                List<AttendeeList> attendee = new List<AttendeeList>();
                // invite friends if the event is a group event
                if (e.EventType == 1)
                {
                    attendee = await InviteFriendsAsync(newEvent);
                }

                EventDetailModel eventDetailModel = new EventDetailModel();
                eventDetailModel.AttendeeList = attendee;
                eventDetailModel.Events = newEvent;


                // show Event Detail Page
                return View("EventDetail", eventDetailModel);
            } else
            {
                return View();
            }
           
        }

        [HttpGet]
        public IActionResult EventSummary()
        {
            return View();
        }

        public async Task<List<AttendeeList>> InviteFriendsAsync(CreateEvent e) 
        {
            System.Diagnostics.Debug.WriteLine("Calling Invite Friends");

            // get current user
            var user = await _securityManager.GetUserAsync(User);
            

            // get list of friends
            var query_getFriends_attendeeList = (from f in _context.FriendLists
                            join u in _context.Users on f.FriendId equals u.Id
                            where f.UserId == user.Id
                            select new AttendeeList
                            {
                                EventId = e.EventId,
                                UserId = f.FriendId,
                                Status = "Invited"
                            });

            List<AttendeeList> attendeeList = new List<AttendeeList>();
            // 1. Add friends to Attendee

            if (query_getFriends_attendeeList.Any()) // check if the user has any friend
            {
                // convert to List<AttendeeList>
                attendeeList = new List<AttendeeList>(query_getFriends_attendeeList);
                // Add to AttendeeList
                foreach (AttendeeList attendee in attendeeList)
                {
                    _context.AttendeeList.Add(attendee);
                }

                _context.SaveChanges();


                // 2. Send Notification to Friends
                var query_getFriends_notification = (from f in _context.FriendLists
                                                     join u in _context.Users on f.FriendId equals u.Id
                                                     where f.UserId == user.Id
                                                     select new Notification
                                                     {
                                                         EventId = e.EventId,
                                                         RecipientId = f.FriendId,
                                                         SenderId = f.UserId,
                                                         Message = user.UserName + " invited you to an event!"
                                                     });
                // convert to List<Notification>
                List<Notification> notifications = new List<Notification>(query_getFriends_notification);
                // Add to Notification
                foreach (Notification notification in notifications)
                {
                    _context.Notifications.Add(notification);
                }

                _context.SaveChanges();

            }
             return attendeeList;
        }

      
    }
}
