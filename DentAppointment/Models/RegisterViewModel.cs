using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentAppointment.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Please Enter User Name")]
        [Display(Name="Enter your user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [Display(Name = "Enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter surname")]
        [Display(Name = "Enter your surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Enter your email")]
        [EmailAddress(ErrorMessage ="Please check your e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Color")]
        [Display(Name = "Appointment Color")]
        public string Color { get; set; }

        [Display(Name = "Is Doctor/Dentist?")]
        public bool IsDentist { get; set; }
    }
}
