using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WeLinkUp.Models;

namespace WeLinkUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

       // public readonly IWebHostEnvironment webHostEnvironment;
        //private readonly IWebHostEnvironment WebHostEnvironment;

        public HomeController(IConfiguration configuration)
        {
            this._configuration = configuration;
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
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User user)
        {
            
            //ProfileImage = stringFileName;

            if (ModelState.IsValid)
            {
                string ProfileImage = UploadFile(user);
                
                using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                 
                    string sqlquery = "insert into users(Username,Email,DateofBirth,Freetime,Password,ProfileImage) values ('" + user.Username + "','" + user.Email + "','" + user.DateofBirth + "','" + user.Freetime + "','" + user.Password +"'," +
                        "'"+ user.ProfileImage + "')";
                    using (SqlCommand sqlcommand = new SqlCommand(sqlquery, sqlconnection))
                    {
                        sqlconnection.Open();
                        sqlcommand.ExecuteNonQuery();
                        ViewData["Message"] = "New User " + user.Username + " is saved successfully!";
                    }
                   
                }
                return RedirectToAction(nameof(Login));
            }
            return View(user);

        }


        private string UploadFile(User user)
        {
            string fileName = null;
            if (user.ProfileImage != null)
            {
                //string uploadDir = ConfigurationPath.Combine(WebHostEnvironment.WebRootPath, "Images");
                //fileName = Guid.NewGuid().ToString() + "-" + user.ProfileImage.FileName;
                //string filePath = Path.Combine(uploadDir, fileName);
                //using (var fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    user.ProfileImage.CopyTo(fileStream);

                //}
            }
            return fileName;
        }
    }
}
