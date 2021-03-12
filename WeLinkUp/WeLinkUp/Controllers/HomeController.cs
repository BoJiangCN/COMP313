using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        private readonly IConfiguration _configuration;
        //private readonly ApplicationDbContext _context;        
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> secMgr, SignInManager<ApplicationUser> loginManager)
        {
            this._configuration = configuration;
            this._webHostEnvironment = webHostEnvironment;
            this._securityManager = secMgr;
            this._loginManager = loginManager;
            
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
                //code for add imgae to db
                string wwwroot = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                string extension = Path.GetExtension(user.ImageFile.FileName);
                user.Image = fileName = fileName + extension;
                string path = Path.Combine(wwwroot + "/images/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    user.ImageFile.CopyTo(fileStream);

                }

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


                //var Freetime= freetime.ToList();
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
                    //Freetime = user.Freetime.ToString()
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

                // return RedirectToAction(nameof(Login));
            }
            return View(user);

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
                //// find user using username
                //ApplicationUser user = await _securityManager.FindByNameAsync(model.Username);
                //// if there is a returned user
                //if (user != null)
                //{

                // login with user's email and password. Somehow this methods doesn't work with username 
                var result = await _loginManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid UserName or Password");
                    return View();
                }

                //}
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
    }
}
