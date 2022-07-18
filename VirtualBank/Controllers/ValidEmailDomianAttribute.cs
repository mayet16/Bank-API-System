using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualBank.Controllers
{
    public class ValidEmailDomianAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidEmailDomianAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value)
        {
            try
            {
                string[] strings = value.ToString().Split("@");
                return strings[1].ToUpper() == allowedDomain.ToUpper();
            }
            catch(Exception)
            {
              
            }
            return false;  
        }
    }
}


