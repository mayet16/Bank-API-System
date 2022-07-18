using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualBank.ViewModel
{
    public class CheckOutViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double? OrderTotal { get; set; }
        public bool RememberMe { get; set; }
    }
}
