using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class EventDetailModel
    {
        public CreateEvent Events { get; set; }
        public IEnumerable<AttendeeList> AttendeeList { get; set; }
        public User User { get; set; }
    }
}
