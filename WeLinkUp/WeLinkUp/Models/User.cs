using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Please enter your free time")]
        [DataType(DataType.Time)]
        public string Freetime { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password doesn't match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm your password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
