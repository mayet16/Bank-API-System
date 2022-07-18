using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VirtualBank.Models;
using VirtualBank.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualBank.Controllers
{
    public class AccountController : Controller
    {
    
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;
        private readonly RoleManager<UserRoles> roleManager;
        private readonly GebyaContext _gebyaContext;

        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager, RoleManager<UserRoles> roleManager,
          GebyaContext gebyaContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _gebyaContext = gebyaContext;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
          
            if (ModelState.IsValid)
            {

                var u = _gebyaContext.User.ToList().LastOrDefault();
                var aacc = u.Account_Number;
                if (aacc == 0)
                {
                    aacc = 1000;

                }
                var user = new Users
                    {
                        UserName = registerViewModel.Username,
                        Email = registerViewModel.Email,
                        PhoneNumber = registerViewModel.PhoneNumber,
                        FirstName = registerViewModel.Fname,
                        LastName = registerViewModel.Lname,
                    Account_Number = aacc + 1,
                    Address = registerViewModel.Address,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, registerViewModel.Password);

                    if (result.Succeeded)
                    {
                        UserRoles userRole = new UserRoles
                        {
                            Name = "User"
                        };
                        IdentityResult r = await roleManager.CreateAsync(userRole);
                        await userManager.AddToRoleAsync(user, userRole.Name);

                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Dologin", "Home");
                    }
               
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

            }

            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
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
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

    }
}

