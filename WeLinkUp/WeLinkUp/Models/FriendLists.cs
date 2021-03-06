using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class FriendLists
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "FriendId")]
        public string FriendId { get; set; }
    }
}
