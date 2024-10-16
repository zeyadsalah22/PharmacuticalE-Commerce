using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Viewmodels;

namespace PharmacuticalE_Commerce.Controllers
{
    public class UserAuthController : Controller
    {
        public UserManager<User> _userManager { get; }
        public SignInManager<User> _signInManager { get; }

        public UserAuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
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
                User user = new User
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    PasswordHash = registerViewModel.Password,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    Fname = registerViewModel.Fname,
                    Lname = registerViewModel.Lname
                };
                IdentityResult result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    //cookies
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Ecommerce", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerViewModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult StaffLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(loginViewModel.Username);
                if (user != null) { 
                    bool userFound = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (userFound)
                    {
                        await _signInManager.SignInAsync(user, loginViewModel.RememberMe);
                        return RedirectToAction("Ecommerce", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid UserName and/or Password");
         
            }
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Details(string username) {
			User user = await _userManager.FindByNameAsync(username);
            if (user == null)
			{
				return NotFound();
			}
            return View(user);
        }


        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult StaffLogout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(StaffLogin));
        }
    }
}
