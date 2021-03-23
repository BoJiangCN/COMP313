using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class ViewCalendarModel
    {
        public int CalendarId { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }

        public string EventTitle { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
