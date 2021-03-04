using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class CreateEvent
    {
        public long EventId { get; set; }

        [Required(ErrorMessage = "Please enter event title")]
        [Display(Name = "EventTitle")]
        public string EventTitle { get; set; }

        [Required(ErrorMessage = "Please enter event location")]
        [Display(Name = "Location")]

        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter event date")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        [Required(ErrorMessage = "Please enter start time")]
        [Display(Name = "StartTime")]
        [DataType(DataType.Time)]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "Please enter end time")]
        [Display(Name = "EndTime")]
        [DataType(DataType.Time)]
        public string EndTime { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "IsAdultOnly")]
        public bool IsAdultOnly { get; set; }

        public string Image { get; set; }


        [Display(Name = "EventType")]
        public bool EventType { get; set; } //true for group event(send invitation to friends) & false for personal event(no invitation to others)

    }
}
