using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{

    public class AttendeeList
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
    }
}
