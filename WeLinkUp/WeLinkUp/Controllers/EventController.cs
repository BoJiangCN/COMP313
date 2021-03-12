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
        
          [HttpGet]
        public async Task<IActionResult> Friends()
        {
            // get current user
            var user = await _securityManager.GetUserAsync(User);

            // get list of friends
            List<FriendLists> friends = _context.FriendLists.Where(fr => fr.UserId == user.Id)
                .Select(fr => new FriendLists { UserId = fr.UserId, FriendId = fr.FriendId }).ToList();
            List<ApplicationUser> userFriends = new List<ApplicationUser>();

            // get information of each friends
            foreach (FriendLists friend in friends)
            {
                ApplicationUser userFriend = _context.Users.Where(us => us.Id == friend.FriendId)
                    .Select(fr => new ApplicationUser { UserName = fr.UserName}).FirstOrDefault();
                userFriends.Add(userFriend);
            }

            
            return View(userFriends);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync(CreateEvent e, bool isAdultOnlyChecked)
        {

            //code for add imgae to db
            string wwwroot = _webHostEnvironment.WebRootPath;
            string fileName = String.Empty;

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

        
            // show Event Detail Page
            return View("EventDetail", newEvent);
           
        }

        [HttpGet]
        public IActionResult EventSummary()
        {
            return View();
        }

      
    }
}
