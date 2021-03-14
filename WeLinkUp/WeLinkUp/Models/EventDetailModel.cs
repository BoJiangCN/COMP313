using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class EventDetailModel
    {
        public int EventId { get; set; }

        public string EventTitle { get; set; }

        public string Location { get; set; }

   
        public string Date { get; set; }
        public string DayOfWeek { get; set; }


        public string Host { get; set; }
        public string HostId { get; set; }

   
        public string StartTime { get; set; }
        public string StartTimeToShow { get; set; }

        public string EndTime { get; set; }
        public string EndTimeToShow { get; set; }


        public string Description { get; set; }

    
        public int IsAdultOnly { get; set; } // 1 for true, 0 for false

        public string Image { get; set; }

    
        public int EventType { get; set; } //1 = true for group event(send invitation to friends) & 0 = false for personal event(no invitation to others)

        public IEnumerable<AttendeeList> AttendeeList { get; set; }
        public User User { get; set; }
        public int NumberOfInvitation { get; set; }
    }
}
