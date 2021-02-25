using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Signup(User ur)
        {
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string sqlquery = "insert into users(Username,Email,DateofBirth,Freetime,Password) values ('" + ur.Username + "','" + ur.Email + "','" + ur.DateofBirth + "','" + ur.Freetime + "','" + ur.Password + "')";
                using (SqlCommand sqlcommand = new SqlCommand(sqlquery, sqlconnection))
                {
                    sqlconnection.Open();
                    sqlcommand.ExecuteNonQuery();

                }
                ViewData["Message"] = "New User " + ur.Username + " is saved successfully!";
            }
            return View(ur);

            //return RedirectToAction(nameof(Login));
        }
    }
}
