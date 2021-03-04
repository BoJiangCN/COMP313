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
using WeLinkUp.Models;

namespace WeLinkUp.Controllers
{
    public class HomeController : Controller
    {
        //1
        private readonly UserManager<ApplicationUser> _securityManager;
        private readonly SignInManager<ApplicationUser> _loginManager;
        
        private readonly IConfiguration _configuration;

       // public readonly IWebHostEnvironment webHostEnvironment;
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
        // GET: User/Login
        public IActionResult Login()
        {
            return View();
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
        public async Task<IActionResult> Signup(User user)
        {               
          if (ModelState.IsValid)
            {
                //code for add imgae to db
                //save image to wwwroot/image
                string wwwroot = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                string extension = Path.GetExtension(user.ImageFile.FileName);
                user.Image= fileName = fileName + extension;
                string path = Path.Combine(wwwroot + "/images/", fileName);
                //using (var fileStream = new FileStream(path, FileMode.Create))
                //{
                //    user.ImageFile.CopyTo(fileStream);

                //}                

                //add signup information to db
                var newUser = new ApplicationUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    Image = user.Image,
                    DateofBirth = user.DateofBirth,
                    Freetime = user.Freetime


                };
                var result = await _securityManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    await _loginManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }


                //using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                //{

                //    string sqlquery = "insert into users(Username,Email,DateofBirth,Freetime,Password,Image) values ('" + user.Username + "','" + user.Email + "','" + user.DateofBirth + "','" + user.Freetime + "','" + user.Password +"','"+user.Image+"')";
                //    using (SqlCommand sqlcommand = new SqlCommand(sqlquery, sqlconnection))
                //    {
                //        sqlconnection.Open();
                //        sqlcommand.ExecuteNonQuery();
                //        ViewData["Message"] = "New User " + user.Username + " is saved successfully!";
                //    }

                //}
                return RedirectToAction(nameof(Login));
            }
            return View(user);

        }
                
    }
}
