using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualBank.Models;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VirtualBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GebyaContext _gebyaContext;
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;
        private readonly RoleManager<UserRoles> roleManager;

        public HomeController(ILogger<HomeController> logger, GebyaContext gebyaContext, UserManager<Users> userManager,
                              SignInManager<Users> signInManager, RoleManager<UserRoles> roleManager)
        {
            _logger = logger;
            _gebyaContext = gebyaContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public ILogger<HomeController> Logger => _logger;

        public UserManager<Users> UserManager => userManager;

        public RoleManager<UserRoles> RoleManager => roleManager;

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        public IActionResult DoRegister()
        {

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            var user = _gebyaContext.User.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var users = new DepositViewModel
            {
                AccountNumber = user.Account_Number,
                Balance = user.Balance
            };
            return View(users);
        }

        [HttpPost]
        public IActionResult Deposit(DepositViewModel depositViewModel)
        {
            var user = _gebyaContext.User.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var bal = user.Balance;
            var deposit = depositViewModel.Deposit;
            var total = bal + deposit;

            user.Balance = total;
            _gebyaContext.User.Update(user);
            _gebyaContext.SaveChanges();
            return RedirectToAction("DoLogin");
        }
        public IActionResult Balance()
        {
            var user = _gebyaContext.User.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var users = new DepositViewModel
            {
                AccountNumber = user.Account_Number,
                Balance = user.Balance
            };
            return View(users);
        }
        [HttpGet]
        public IActionResult Transfer()
        {
            var user = _gebyaContext.User.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var users = new DepositViewModel
            {
              SourceAccountNumber = user.Account_Number,

            };
            return View(users);
        }
        [HttpPost]
        public IActionResult Transfer(DepositViewModel depositViewModel)
        {
            var user = _gebyaContext.User.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var bal = user.Balance;
            var acc = depositViewModel.DestinationAccountNumber;
            var amt = depositViewModel.TranferAmount;
            var nbalance = bal-amt;
            user.Balance = nbalance;
            var user2 = _gebyaContext.User.SingleOrDefault(u => u.Account_Number == acc);
            user2.Balance = user2.Balance +( amt);
          
            _gebyaContext.User.Update(user);
            _gebyaContext.User.Update(user2);
            _gebyaContext.SaveChanges();
            return RedirectToAction("Dologin","home");
        }
        [HttpGet]
        public IActionResult Withdraw()
        {
            
                var user = _gebyaContext.User.SingleOrDefault(u => u.UserName == User.Identity.Name);
                var users = new DepositViewModel
                {
                    AccountNumber = user.Account_Number,
                    Balance = user.Balance
               };
                return View(users);
              
        }

        [HttpPost]
        public IActionResult Withdraw(DepositViewModel depositViewModel)
        {
            var user = _gebyaContext.User.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var bal = user.Balance;
            var Withdraw = depositViewModel.Withdraw;
            var total = bal - Withdraw;

            user.Balance = total;
            _gebyaContext.User.Update(user);
            _gebyaContext.SaveChanges();
            return RedirectToAction("DoLogin");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DoLogin()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("DoLogin", "home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

    }
}

