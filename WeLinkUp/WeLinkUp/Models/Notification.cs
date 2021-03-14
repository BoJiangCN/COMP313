using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        public string RecipientId { get; set; }

        public string SenderId { get; set; }
        public string Message { get; set; }
        public int EventId { get; set; }       
        public DateTime Date { get; set; }
    }
}
