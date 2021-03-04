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
        public string Freetime { get; set; }
    }
}
