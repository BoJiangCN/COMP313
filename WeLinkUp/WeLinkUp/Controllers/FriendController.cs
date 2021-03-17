﻿using System;
using System.Collections.Generic;
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
    public class FriendController : Controller
    {
        private readonly UserManager<ApplicationUser> _securityManager;
        private readonly ApplicationDbContext _context;

        public FriendController(UserManager<ApplicationUser> secMgr, ApplicationDbContext context)
        {
            this._securityManager = secMgr;
            this._context = context;
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
                    .Select(fr => new ApplicationUser { UserName = fr.UserName, Image = fr.Image }).FirstOrDefault();
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
    }
}
