using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using VirtualBank.Controllers;
namespace VirtualBank.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        //  [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomian(allowedDomain: "gmail.com", ErrorMessage = "Email Domian Must Be @gmail.com")]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password And Confirmition Password Do not Match")]
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Address { get; set; }

    }
}
