using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WeLinkUp.Data;
using WeLinkUp.Models;

namespace WeLinkUp.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly UserManager<ApplicationUser> _securityManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public FriendController(UserManager<ApplicationUser> secMgr, ApplicationDbContext context, IConfiguration configuration)
        {
            this._securityManager = secMgr;
            this._context = context;
            this._configuration = configuration;
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
                    .Select(fr => new ApplicationUser { UserName = fr.UserName, Image = fr.Image, Id = fr.Id }).FirstOrDefault();
                userFriends.Add(userFriend);
            }


            return View(userFriends);
        }

        //Search User from database
        public IActionResult SearchUser(string searchString)
        {
            var user = from s in _context.Users select s;
            if (!String.IsNullOrEmpty(searchString))
            {

                user = _context.Users.Where(s => s.UserName.Contains(searchString));
            }
            if (user.Count() == 0)
            {
                ViewBag.result = "Sorry " + searchString + " does not exist in database!";
                return View();
            }
            return View(user.ToList());
        }
        public async Task<ActionResult> DelteFriendAsync(bool confirm, string friendId)
        {
            if (confirm)
            {

                // get current user
                var user = await _securityManager.GetUserAsync(User);
                
                // delete friend from both users' side
                using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    string sqlquery = "DELETE FROM FriendLists where (UserId='"+user.Id+"' and FriendId='"+friendId+"') or " +
                        "(FriendId='" + user.Id + "' and UserId='" + friendId + "')";
                    
                    System.Diagnostics.Debug.WriteLine(sqlquery);
                    using (SqlCommand sqlcommand = new SqlCommand(sqlquery, sqlconnection))
                    {
                        sqlconnection.Open();
                        sqlcommand.ExecuteNonQuery();

                    }
                }
            }

            return RedirectToAction("Friends");
        }
        
            
        public async Task<IActionResult> Message(string friendId)
        {     
            var user = await _securityManager.GetUserAsync(User);

            List<Notification> messages = _context.Notifications.Where(m => m.RecipientId == user.Id && m.SenderId == friendId || m.RecipientId == friendId && m.SenderId == user.Id)
            .Select(m => new Notification
            {
                Message = m.Message,
                RecipientId = m.RecipientId,
                SenderId = m.SenderId,
                NotificationId = m.NotificationId,
                Date = m.Date
            }).OrderBy(m => m.Date).ToList();



            foreach(Notification n in messages)
            {
                if (n.Date == null)
                {
                    messages.Remove(n);
                }
            }

         
            return View("MessageView", messages);
        }

       

    }

}
