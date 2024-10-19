using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface IBranchRepository : IRepository<Branch>
	{
		Task<IEnumerable<Branch>> GetAllBranches();
		Task<Branch> GetBranchById(int? id);
		Task AddBranch(Branch branch);
		Task UpdateBranch(Branch branch);
		Task DeleteBranch(int id);
		Task<bool> BranchExists(int id);
		Task Save();
	}
}
