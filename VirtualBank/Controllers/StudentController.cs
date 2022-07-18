using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VirtualBank.Models;

namespace VirtualBank.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly GebyaContext _context;
        public StudentController(GebyaContext context) {
            _context = context;
        }

        public List<Users> Get()
        {
            return _context.Users.ToList();
        }

        public IActionResult index()
        {
            return View();
        }
    }
}