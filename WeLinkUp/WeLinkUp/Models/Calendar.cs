using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class Calendar
    {
        [Key]
        public int CalendarId { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
    }
}
