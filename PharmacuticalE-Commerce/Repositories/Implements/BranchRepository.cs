using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
	public class BranchRepository : IBranchRepository
	{
		private readonly PharmacySystemContext _context;

		public BranchRepository(PharmacySystemContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Branch>> GetAll()
		{
			return await Task.FromResult(_context.Branches.ToList());
		}

		public async Task<Branch> GetById(int? id)
		{
			return await _context.Branches.FindAsync(id);
		}

		public async Task Create(Branch branch)
		{
			await _context.AddAsync(branch);
			await _context.SaveChangesAsync();
		}

		public async Task Update(Branch branch)
		{
			_context.Update(branch);
			await _context.SaveChangesAsync();
			await Task.CompletedTask;
		}

		public async Task Delete(int? id)
		{
			var branch = await _context.Branches.FindAsync(id);
			if (branch != null)
			{
				_context.Branches.Remove(branch);
			}
			await _context.SaveChangesAsync();
		}

	}
}
