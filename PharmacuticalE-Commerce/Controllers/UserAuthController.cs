﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using PharmacuticalE_Commerce.Viewmodels;
using PharmacuticalE_Commerce.ViewModels;

namespace PharmacuticalE_Commerce.Controllers
{
    public class UserAuthController : Controller
    {
        public UserManager<User> _userManager { get; }
        public SignInManager<User> _signInManager { get; }
        public RoleManager<IdentityRole> _roleManager { get; }
        private readonly IUserRepository _userRepository;
        public UserAuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
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
                    await _userManager.AddToRoleAsync(user, "Customer");
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
                if (user != null)
                {
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

        public async Task<IActionResult> Details(string username)
        {
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

        public async Task<IActionResult> CustomersList()
        {
            var users = await _userRepository.GetAllCustomerUserViewModels();
            return View(users);
        }
        public async Task<IActionResult> AdminsList()
        {
            var users = await _userRepository.GetAllAdminsViewModels();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserViewModel
            {
                RoleNames = _roleManager.Roles.Select(r => r.Name).ToList()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            RegisterViewModel registerViewModel = new RegisterViewModel
            {
                UserName = model.Username,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.Password,
                PhoneNumber = model.PhoneNumber,
                Fname = model.Fname,
                Lname = model.Lname
            };
            
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
                    await _userManager.AddToRoleAsync(user,model.RoleName);
                    //cookies
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("AdminsList");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            
            model.RoleNames = _roleManager.Roles.Select(r => r.Name).ToList(); // Populate RoleNames again
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userViewModel = new UserViewModel
            {
                UserId = user.Id,
                Fname = user.Fname,
                Lname = user.Lname,
                Email = user.Email,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                RoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };

            userViewModel.RoleNames = new List<string> { userViewModel.RoleName };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            
                var user = await _userManager.FindByIdAsync(model.UserId);

                user.Fname = model.Fname;
                user.Lname = model.Lname;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    await _userManager.AddToRoleAsync(user, model.RoleName);

                    return RedirectToAction("CustomersList");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            

            model.RoleNames = new List<string> { model.RoleName };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("CustomersList");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }

    }
}
