using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using PharmacuticalE_Commerce.ViewModels;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public User GetById(int? id)
        {
            if (id == null) return null;
            return _userManager.Users.FirstOrDefault(u => u.Id == id.ToString());
        }

        public IEnumerable<User> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public void Create(User entity)
        {
            var result = _userManager.CreateAsync(entity).Result;
            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }

        public void Update(User entity)
        {
            var result = _userManager.UpdateAsync(entity).Result;
            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }

        public void Delete(int? id)
        {
            if (id == null) return;
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id.ToString());
            if (user != null)
            {
                var result = _userManager.DeleteAsync(user).Result;
                if (!result.Succeeded)
                {
                    throw new System.Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
                }
            }
        }

        public User GetUserByEmail(string email)
        {
            return _userManager.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByUsername(string username)
        {
            return _userManager.Users.SingleOrDefault(u => u.UserName == username);
        }

        public async Task<UserViewModel> GetUserViewModelById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault();

            return new UserViewModel
            {
                UserId = user.Id,
                Fname = user.Fname,
                Lname = user.Lname,
                Email = user.Email,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                RoleName = roleName
            };
        }

        public async Task<List<UserViewModel>> GetAllUserViewModels()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var roleName = roles.FirstOrDefault();

                userViewModels.Add(new UserViewModel
                {
                    UserId = user.Id,
                    Fname = user.Fname,
                    Lname = user.Lname,
                    Email = user.Email,
                    Username = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    RoleName = roleName
                });
            }

            return userViewModels;
        }
        public async Task<List<UserViewModel>> GetAllCustomerUserViewModels()
        {
            var users = _userManager.Users.ToList();
            var customerUserViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Customer"))
                {
                    customerUserViewModels.Add(new UserViewModel
                    {
                        UserId = user.Id,
                        Fname = user.Fname,
                        Lname = user.Lname,
                        Email = user.Email,
                        Username = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        RoleName = "Customer"
                    });
                }
            }

            return customerUserViewModels;
        }
        public async Task<List<UserViewModel>> GetAllAdminsViewModels()
        {
            var users = _userManager.Users.ToList();
            var adminHRModeratorUserViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel
                {
                    UserId = user.Id,
                    Fname = user.Fname,
                    Lname = user.Lname,
                    Email = user.Email,
                    Username = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    RoleNames = roles // Add all roles for debugging
                };

                if (roles.Contains("Admin") || roles.Contains("HR") || roles.Contains("Moderator"))
                {
                    userViewModel.RoleName = roles.FirstOrDefault();
                    adminHRModeratorUserViewModels.Add(userViewModel);
                }
            }

            return adminHRModeratorUserViewModels;
        }


    }
}
