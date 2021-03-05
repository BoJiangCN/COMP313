using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeLinkUp.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        [HttpGet]

        public IActionResult CreateEvent()
        {
            return View();
        }
    }
}
