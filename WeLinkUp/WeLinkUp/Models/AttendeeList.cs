using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    [Keyless]
    public class AttendeeList
    {
        public int EventId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
    }
}
