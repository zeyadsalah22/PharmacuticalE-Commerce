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

		public async Task<User> GetById(int? id)
		{
			if (id == null) return null;
			return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return await _userManager.Users.ToListAsync();
		}

		public async Task Create(User entity)
		{
			var result = await _userManager.CreateAsync(entity);
			if (!result.Succeeded)
			{
				throw new System.Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
			}
		}

		public async Task Update(User entity)
		{
			var result = await _userManager.UpdateAsync(entity);
			if (!result.Succeeded)
			{
				throw new System.Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
			}
		}

		public async Task Delete(int? id)
		{
			if (id == null) return;
			var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
			if (user != null)
			{
				var result = await _userManager.DeleteAsync(user);
				if (!result.Succeeded)
				{
					throw new System.Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
				}
			}
		}

		public async Task<User> GetUserByEmail(string email)
		{
			return await _userManager.Users.SingleOrDefaultAsync(u => u.Email == email);
		}

		public async Task<User> GetUserByUsername(string username)
		{
			return await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username);
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
			var users = await _userManager.Users.ToListAsync();
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
			var users = await _userManager.Users.ToListAsync();
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
			var users = await _userManager.Users.ToListAsync();
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
					RoleNames = roles
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
