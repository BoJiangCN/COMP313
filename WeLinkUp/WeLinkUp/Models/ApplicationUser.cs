using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class ApplicationUser : IdentityUser
    {
      
        public string Image { get; set; }
        public string DateofBirth { get; set; }
        //public string Freetime { get; set; }        
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
    }
}
