using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
	public class RoleRepository : IRoleRepository
	{
		private readonly PharmacySystemContext _context;

		public RoleRepository(PharmacySystemContext context)
		{
			_context = context;
		}

		public async Task<Role> GetById(int? id)
		{
			return _context.Roles.FirstOrDefault(r => r.RoleId == id);
		}

		public async Task<IEnumerable<Role>> GetAll()
		{
			return _context.Roles.ToList();
		}

		public async Task Create(Role role)
		{
			_context.Roles.Add(role);
			await _context.SaveChangesAsync();
		}

		public async Task Update(Role role)
		{
			_context.Roles.Update(role);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(int? id)
		{
			var role = await GetById(id);
			if (role != null)
			{
				_context.Roles.Remove(role);
				await _context.SaveChangesAsync();
			}
		}
	}
}
