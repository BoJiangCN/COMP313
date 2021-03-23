using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class ViewCalendarModel
    {
        
        

        [Key]
        public int CalendarId { get; set; }
        [NotMapped]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }

        [NotMapped]
        public DateTime Start { get; set; }
        [NotMapped]
        public DateTime End { get; set; }
        [NotMapped]
        public string Text { get; set; }
        [NotMapped]
        public string Color { get; set; }
        [NotMapped]
        public string EventTitle { get; set; }
        [NotMapped]
        public string Date { get; set; }
        [NotMapped]
        public string StartTime { get; set; }
        [NotMapped]
        public string EndTime { get; set; }

    }
}
