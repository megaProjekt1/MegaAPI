using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaProjekt.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Person Name can't be blank")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already used")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number should contain digits only")]
        public string PhoneNumber{ get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be blank")]
        [Compare("Password" ,ErrorMessage = "Password and confirm password don't match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
