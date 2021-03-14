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
                string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
                string extension = Path.GetExtension(e.ImageFile.FileName);
                e.Image = fileName = fileName + extension;
                string path = Path.Combine(wwwroot + "/images/EventPicture/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    e.ImageFile.CopyTo(fileStream);

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

                
                // invite friends if the event is a group event
                if (e.EventType == 1)
                {
                   await InviteFriendsAsync(newEvent);
                }
   
                // show Event Detail Page
                return RedirectToAction("EventDetail", new { eventId = newEvent.EventId });

            } else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();
                System.Diagnostics.Debug.WriteLine("Number of Errors: " + ModelState.ErrorCount);
                System.Diagnostics.Debug.WriteLine(errors[0]);


                return View();
            }

        }

        [HttpGet]
        public IActionResult EventSummary()
        {
            return View();
        }

        public IActionResult Error() 
        {
            return View();
        }

        [HttpGet("Event/EventDetail/{eventId:int?}")]
        public async Task<IActionResult> EventDetailAsync(int eventId)
        {

            EventDetailModel eventDetailModel = new EventDetailModel();

            var q_eventToView = (from e in _context.Events
                                join u in _context.Users on e.HostId equals u.Id
                                where e.EventId == eventId
                                select new EventDetailModel
                                {
                                    EventTitle = e.EventTitle,
                                    Location = e.Location,
                                    Date = e.Date,
                                    StartTime = e.StartTime,
                                    EndTime = e.EndTime,
                                    Description = e.Description,
                                    Image = e.Image,
                                    IsAdultOnly = e.IsAdultOnly == 1 ? 1 : 0,
                                    EventType = e.EventType == 1 ? 1 : 0,
                                    Host = u.UserName
                                });

            List<EventDetailModel> eventList = new List<EventDetailModel>();

            //List<EventDetailModel> l_eventToView = _context.Events.Where(e => e.EventId == eventId)
            // .Select(e => new EventDetailModel
            // {
            //     EventId = e.EventId,
            //     EventTitle = e.EventTitle,
            //     Location = e.Location,
            //     Date = e.Date,
            //     StartTime = e.StartTime,
            //     EndTime = e.EndTime,
            //     Description = e.Description,
            //     Image = e.Image,
            //     IsAdultOnly = e.IsAdultOnly == 1?1:0,
            //     EventType = e.EventType == 1?1:0,
            //     HostId = e.HostId
            // }).ToList();

            //if (l_eventToView.Count == 0) // event does not exist
            //{

            //    return View("Error");
            //}
            if (!q_eventToView.Any()) // event does not exist
            {

                return View("Error");
            }

            else 
            {
                //eventDetailModel.Events = l_eventToView.FirstOrDefault();
                eventList = new List<EventDetailModel>(q_eventToView);
                eventDetailModel = eventList.FirstOrDefault();
                 

                // get current user
                var user = await _securityManager.GetUserAsync(User);

                // get list of friends
                var query_getFriends_attendeeList = (from a in _context.AttendeeList
                                                     join u in _context.Users on a.UserId equals u.Id
                                                     where a.EventId == eventId
                                                     select new AttendeeList
                                                     {
                                                         EventId = a.EventId,
                                                         UserId = a.UserId,
                                                         Status = a.Status,
                                                         Username = u.UserName
                                                     });

                List<AttendeeList> attendeeList = new List<AttendeeList>();
                // 1. Add friends to Attendee
            
                if (query_getFriends_attendeeList.Any()) // check if the user has any friend
                {
                    // convert to List<AttendeeList>
                    attendeeList = new List<AttendeeList>(query_getFriends_attendeeList);             
                }

                eventDetailModel.AttendeeList = attendeeList;

                // check if user is free for this event
                int scheduleResult = checkScheduleAsync(eventDetailModel, user.Id);
                ViewData["Freeday"] = scheduleResult;

                // check user's age if the event is adult only
                //if (eventDetailModel.Events.IsAdultOnly == 1) {
                if (eventDetailModel.IsAdultOnly == 1)
                {
                    int userAge = getAge(Convert.ToDateTime(user.DateofBirth));
                    if (userAge < 18) // block from joining if the user is a teenager
                    {
                        ViewData["BlockTeenager"] = 1;
                    }
                }
                // check user's attendance status

                if (!attendeeList.Exists(a => a.UserId.Equals(user.Id))) // is user in the attendee list?
                {
                    ViewData["Attendance"] = 0; // not invited
                }
                else if (attendeeList.Find(a => a.UserId.Equals(user.Id)).Status == "Confirmed") 
                {
                    ViewData["Attendance"] = 2; // user confirmed to join
                }
                else 
                {
                    ViewData["Attendance"] = 1; // user invited to the event
                }

                return View(eventDetailModel);
            }
        }
        
        public async Task InviteFriendsAsync(CreateEvent e) 
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
                                Status = "Invited",
                                Username = u.UserName
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
        
        }
      
        public async Task<IActionResult> JoinEventAsync(int eventId)
        {

            // get current user
            var user = await _securityManager.GetUserAsync(User);

            EventDetailModel eventDetailModel = new EventDetailModel();

            // get event entity from database using event id
            List<CreateEvent> l_eventToView = _context.Events.Where(e => e.EventId == eventId)
              .Select(e => new CreateEvent
              {
                  EventId = e.EventId,
                  EventTitle = e.EventTitle,
                  Location = e.Location,
                  Date = e.Date,
                  StartTime = e.StartTime,
                  EndTime = e.EndTime,
                  Description = e.Description,
                  Image = e.Image,
                  IsAdultOnly = e.IsAdultOnly == 1 ? 1 : 0,
                  EventType = e.EventType == 1 ? 1 : 0,
                  HostId = e.HostId
              }).ToList();


            if (l_eventToView.Any()) // check the event exists in Events table
            {
                eventDetailModel.Events = l_eventToView.FirstOrDefault();

                try
                {
                    var attendance = _context.AttendeeList.FirstOrDefault(a => a.UserId == user.Id && a.EventId == eventId);
                    if (attendance == null) // user is not invited or there is no attendee for the event
                    {
                        return RedirectToAction("EventDetail", new { eventId = eventId }); ;
                    }
                    else
                    {
                        attendance.Status = "Confirmed";
                        _context.SaveChanges();
                    }

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    return RedirectToAction("EventDetail", new { eventId = eventId }); ;
                }
            }
                
            return RedirectToAction("EventDetail", new { eventId = eventId });

        }

        public int getAge(DateTime birthdate) 
        {
            int age;

            age = DateTime.Now.Year - birthdate.Year;

            if (birthdate.Date > DateTime.Now.AddYears(-age)) age--;

            return age;
        }

        public int checkScheduleAsync(EventDetailModel eventDetailModel, string userId) 
        {
            int result = 0;
       
            int eventDayOfWeek = Convert.ToInt32(Convert.ToDateTime(eventDetailModel.Date).DayOfWeek);
            System.Diagnostics.Debug.WriteLine(eventDayOfWeek);
            switch (eventDayOfWeek)
            {
                case 0: //sunday
                    var users0 = from u in _context.Users
                                where (u.Id == userId && u.Sunday == true)
                                select u;
                    if (users0.Any()) { result = 1; }

                    break;
                case 1: // monday
                    var users1 = from u in _context.Users
                                 where (u.Id == userId && u.Monday == true)
                                 select u;
                    if (users1.Any()) { result = 1; }
                    break;
                case 2: // tuesday
                    var users2 = from u in _context.Users
                                 where (u.Id == userId && u.Tuesday == true)
                                 select u;
                    if (users2.Any()) { result = 1; }
                    break;
                case 3: // wednesday
                    var users3 = from u in _context.Users
                                 where (u.Id == userId && u.Wednesday == true)
                                 select u;
                    if (users3.Any()) { result = 1; }
                    break;
                case 4: // thursday
                    var users4 = from u in _context.Users
                                 where (u.Id == userId && u.Thursday == true)
                                 select u;
                    if (users4.Any()) { result = 1; }
                    break;
                case 5: // friday
                    var users5 = from u in _context.Users
                                 where (u.Id == userId && u.Friday == true)
                                 select u;
                    if (users5.Any()) { result = 1; }
                    break;
                case 6: // saturday
                    var users6 = from u in _context.Users
                                where (u.Id == userId && u.Saturday == true)
                                select u;
                    if (users6.Any()) { result = 1; }
                    break;

            }

    

            return result;
        }
        



    }

      
}

