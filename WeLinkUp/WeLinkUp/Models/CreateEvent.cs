using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class CreateEvent
    {
        [Key]
        public int EventId { get; set; }

        [MaxLength(40)]
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
        
        [Required]
        [Display(Name = "HostId")]
        public string HostId { get; set; }

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
        public int IsAdultOnly { get; set; } // 1 for true, 0 for false

        public string Image { get; set; }

        [NotMapped]
        //[Required(ErrorMessage = "Please Upload your picture")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "EventType")]
        public int EventType { get; set; } //1 = true for group event(send invitation to friends) & 0 = false for personal event(no invitation to others)


 

    }
}
