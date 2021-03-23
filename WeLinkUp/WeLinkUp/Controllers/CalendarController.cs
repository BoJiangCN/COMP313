using System;
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
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _securityManager;

        public CalendarController(ApplicationDbContext context, UserManager<ApplicationUser> secMgr)
        {
            this._context = context;
            this._securityManager = secMgr;
        }
        [HttpGet]
        public IActionResult Calendar()
        {
            return View();
        }

        [HttpGet]
        [Route("api/Events")]
        public async Task<IEnumerable<ViewCalendarModel>> GetEventsAsync([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            System.Diagnostics.Debug.WriteLine("start:" +start);
            System.Diagnostics.Debug.WriteLine("end:" +end);
            // get current user to get its id(host)
            var user = await _securityManager.GetUserAsync(User);
            var schedule = (from c in _context.Calendar
                            join e in _context.Events on c.EventId equals e.EventId
                            where c.UserId == user.Id 
                            select new ViewCalendarModel
                            {
                                Id = c.CalendarId,
                                Start = DateTime.Parse(e.Date + " " + e.StartTime),
                                End = DateTime.Parse(e.Date + " " + e.EndTime),
                                Text = e.EventTitle,
                                Color = e.HostId == user.Id ? "#EA9999" : e.EventType == 1 ? "#FFE599" : "#A2C4C9"
                            });

          
            return schedule;
            
            //return from e in _context.Events where !((e.End <= start) || (e.Start >= end)) select e;
        }
    }
}
