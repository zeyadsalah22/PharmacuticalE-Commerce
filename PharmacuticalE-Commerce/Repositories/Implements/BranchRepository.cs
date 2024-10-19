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

		public async Task<IEnumerable<Branch>> GetAllBranches()
		{
			return await Task.FromResult(_context.Branches.ToList());
		}

		public async Task<Branch> GetBranchById(int? id)
		{
			return await _context.Branches.FindAsync(id);
		}

		public async Task AddBranch(Branch branch)
		{
			await _context.AddAsync(branch);
		}

		public async Task UpdateBranch(Branch branch)
		{
			_context.Update(branch);
			await Task.CompletedTask;
		}

		public async Task DeleteBranch(int id)
		{
			var branch = await _context.Branches.FindAsync(id);
			if (branch != null)
			{
				_context.Branches.Remove(branch);
			}
		}

		public async Task<bool> BranchExists(int id)
		{
			return _context.Branches.Any(e => e.BranchId == id);
		}

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<Branch> GetById(int? id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Branch>> GetAll()
		{
			throw new NotImplementedException();
		}

		public async Task Create(Branch entity)
		{
			throw new NotImplementedException();
		}

		public async Task Update(Branch entity)
		{
			throw new NotImplementedException();
		}

		public async Task Delete(int? id)
		{
			throw new NotImplementedException();
		}
	}
}
