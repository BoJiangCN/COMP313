using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WeLinkUp.Data;
using WeLinkUp.Models;

namespace WeLinkUp.Controllers
{
    public class HomeController : Controller
    {
        //1
        private readonly UserManager<ApplicationUser> _securityManager;
        private readonly SignInManager<ApplicationUser> _loginManager;
        private readonly ApplicationDbContext _context;

        private readonly IConfiguration _configuration;       
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ApplicationDbContext context,IConfiguration configuration, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> secMgr, SignInManager<ApplicationUser> loginManager)
        {
            this._configuration = configuration;
            this._webHostEnvironment = webHostEnvironment;
            this._securityManager = secMgr;
            this._loginManager = loginManager;

            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //Get User signup info
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(User user, string sunday, string monday, string tuesday, string wednesday, string thursday, string friday, string saturday)
        {
            
            if (ModelState.IsValid)
            {
                //Image
                user.Image = await GetImage(user);

                if (sunday == "true")
                {
                    user.Sunday = true;
                }
                else
                {
                    user.Sunday = false;
                }
                if (monday == "true")
                {
                    user.Monday = true;
                }
                else
                {
                    user.Monday = false;
                }
                if (tuesday == "true")
                {
                    user.Tuesday = true;
                }
                else
                {
                    user.Tuesday = false;
                }
                if (wednesday == "true")
                {
                    user.Wednesday = true;
                }
                else
                {
                    user.Wednesday = false;
                }
                if (thursday == "true")
                {
                    user.Thursday = true;
                }
                else
                {
                    user.Thursday = false;
                }
                if (friday == "true")
                {
                    user.Friday = true;
                }
                else
                {
                    user.Friday = false;
                }
                if (saturday == "true")
                {
                    user.Saturday = true;
                }
                else
                {
                    user.Saturday = false;
                }

                //add signup information to db
                var newUser = new ApplicationUser
                {
                    UserName = user.Username,
                    Email = user.Email,
                    Image = user.Image,
                    DateofBirth = user.DateofBirth,
                    Sunday = user.Sunday,
                    Monday = user.Monday,
                    Tuesday = user.Tuesday,
                    Wednesday = user.Wednesday,
                    Thursday = user.Thursday,
                    Friday = user.Friday,
                    Saturday = user.Saturday
                };
                var result = await _securityManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    // if signup is successful, user automatically logs in and go to homepage
                    await _loginManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                else
                {
                    foreach (IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View();
                }

            }
            return View(user);

        }

        public async Task<string> GetImage(User user)
        {
            //code for save imgae to s3 bucket               
            string AWS_bucketName = "softwareprojectnew2";
            string AWS_defaultFolder = "UserProfile";

            var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
            var bucketName = AWS_bucketName;
            var keyName = AWS_defaultFolder;
            keyName = keyName + "/" + user.ImageFile.FileName;
            user.Image = user.ImageFile.FileName;
            var fs = user.ImageFile.OpenReadStream();
            var request = new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = bucketName,
                Key = keyName,
                InputStream = fs,
                ContentType = user.ImageFile.ContentType,
                CannedACL = S3CannedACL.PublicRead

            };
            await s3Client.PutObjectAsync(request);     
           var path = string.Format("http://{0}.s3.amazonaws.com/{1}", bucketName, keyName);
           return user.Image = Path.GetFileName(path);
        }


        // GET: User/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {             

                // login with user's email and password. Somehow this methods doesn't work with username 
                var result = await _loginManager.PasswordSignInAsync(model.Username.Trim(), model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The username and/or password is incorrect");
                    return View();
                }

            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _loginManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //Display All Account Information
        [HttpGet]
        public async Task<IActionResult> ProfileInfo()
        {
            var userId = _securityManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ApplicationUser users = await _securityManager.FindByIdAsync(userId);
                ViewBag.user = users;
                return View(users);
            }
        }

        //Edit profile
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = _securityManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ApplicationUser users = await _securityManager.FindByIdAsync(userId);
                ViewBag.user = users;
                return View(users);
            }
        }

            [HttpPost]
        public async Task<IActionResult> EditProfile(User model)
        {
            var userId = _securityManager.GetUserId(User);
            ApplicationUser users = await _securityManager.FindByIdAsync(userId);

            //users.UserName = model.Username;
            users.Email = model.Email;
            users.Image = model.Image;
            users.DateofBirth = model.DateofBirth;
            users.Sunday = model.Sunday;
            users.Monday = model.Monday;
            users.Tuesday = model.Tuesday;
            users.Wednesday = model.Wednesday;
            users.Thursday = model.Thursday;
            users.Friday = model.Friday;
            users.Saturday = model.Saturday;

            var result = await _securityManager.UpdateAsync(users);

            if (ModelState.IsValid)
            {
                return View(result);
            }
            return View("Index");

        }


        //get the list notification for the user
        [HttpGet]
        public async Task<IActionResult> Notification(Notification notification)
        {
            var user = await _securityManager.GetUserAsync(User);

             List<Notification> messages = _context.Notifications.Where(m => m.RecipientId == user.Id)
             .Select(m => new Notification
             {
                 Message = m.Message,
                 EventId = m.EventId,
                 NotificationId = m.NotificationId
             }).OrderByDescending(m=>m.NotificationId).ToList();

            //
            return View(messages);

        }

        //get the details of event per notification
        [HttpPost]
        public IActionResult NotificationDetails(string id)
        {
            return RedirectToAction("EventDetail", new { eventId = id });
        }

    }
}
