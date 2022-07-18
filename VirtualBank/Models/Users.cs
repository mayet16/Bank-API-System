using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VirtualBank.Models;
namespace VirtualBank.Models
{
    public class Users  :IdentityUser<int>
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name Can not exceded 10 characters")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name Can not exceded 10 characters")]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Account_Number { get; set; }
        public float Balance { get; set; }

    }
    
}
