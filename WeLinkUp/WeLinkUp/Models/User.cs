using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class User
    {       
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter your username")]
        [Display(Name = "Username")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Please enter your date email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your date of birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public string DateofBirth { get; set; }

        //[Required(ErrorMessage = "Please select your free time")]
        [Display(Name = "Freetime")]        
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        //public List<Freetime> Freetime { get; set; }
        

        [Required(ErrorMessage = "Please enter your password")]        
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }     
        public string Image { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please Upload your picture")]
        public IFormFile ImageFile{ get; set; }

    }
}
