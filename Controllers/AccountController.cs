using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Services.UserAccountMapping;
using Woody.Models;
using Woody.ViewModels.Account;

namespace Woody.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _identityRole;

        public AccountController(RoleManager<IdentityRole> identityRole, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _identityRole = identityRole;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Email = vm.Email,
                UserName = vm.Username
            };

            var result = await _userManager.CreateAsync(user,vm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
                if(user == null) { throw new Exception("Mail or Username is incorrect."); }
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, vm.Password,false);
            if(!result.Succeeded) { throw new Exception("Mail or Username is incorrect."); }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }
    }
}
