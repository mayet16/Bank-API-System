using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VirtualBank.Models;
using VirtualBank.ViewModel;

namespace VirtualBank.Controllers
{
    //[Route("api/[Controller]")]
    //[ApiController]
    public class BankAPIController : ControllerBase
    {
        private  GebyaContext _context;
        //private readonly UserManager<User> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public BankAPIController(GebyaContext context, SignInManager<Users> signInManager) { 
            _context = context;
            //_userManager = userManager;
           _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody]CheckOutViewModel order)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Not valid");
            }
            if (order.OrderTotal==0)
            {
                return BadRequest("Not valid");
            }
           
            var result = await _signInManager.PasswordSignInAsync(order.Username, order.Password, order.RememberMe, false);
            if (result.Succeeded)
            {
                var u = _context.User.SingleOrDefault(a => a.UserName == order.Username);
                if (u.Balance<(float)order.OrderTotal)
                {
                    return BadRequest("Not valid");
                }
                u.Balance = u.Balance - (float)order.OrderTotal;
                var Admin = _context.User.Find(1);
                Admin.Balance = Admin.Balance + (float)order.OrderTotal;
                _context.Update(u);
                _context.Update(Admin);
                _context.SaveChanges();
            }
            else
            {
                return BadRequest("not Correct username");
            }

            return Ok();
        }
        [HttpPost("{id}")]
        public IActionResult GetUsers(int id)
        {
           
            var Admin = _context.User.Find(2);
            Admin.Balance = Admin.Balance + (float)1000;
            //    _context.Update(u);
            _context.Update(Admin);
            _context.SaveChanges();
            return Ok();
        }

    }
}