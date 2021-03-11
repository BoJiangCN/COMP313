using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;


        }
        [HttpGet]

        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEvent(CreateEvent e, bool isAdultOnlyChecked)
        {
            //if (ModelState.IsValid)
            //{

            //code for add imgae to db
            string wwwroot = _webHostEnvironment.WebRootPath;
            string fileName = String.Empty;
            if (e.ImageFile.FileName == null)
            {
                var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
                string content = provider.GetDirectoryContents(Path.Combine(wwwroot + "/images/No_Image_Available.jpg")).ToString();
                e.Image = content;
                
            } else
            {
                //string wwwroot = _webHostEnvironment.WebRootPath;
                fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
                string extension = Path.GetExtension(e.ImageFile.FileName);
                e.Image = fileName = fileName + extension;
                string path = Path.Combine(wwwroot + "/images/", fileName);
            }


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
                    EventHost = User.Identity.Name

                };


                // login with user's email and password. Somehow this methods doesn't work with username 
                //var result = 
                _context.Add(newEvent);
                _context.SaveChanges();

                //if (result != null)
                //{
                //return RedirectToAction(nameof(HomeController.Index), "Home");
                //}
                //else
                //{
                //ModelState.AddModelError("", "Invalid UserName or Password");
                return View("EventDetail", newEvent);
                //return RedirectToAction(nameof(HomeController.Index), "Home");
                //}
            //}// else
            //{
                //ModelState.AddModelError("", "Please fill out the form properly");
                //return View(e);
            //}
            
        }

        [HttpGet]
        public IActionResult EventSummary()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Calendar()
        {
            return View();
        }
    }
}
