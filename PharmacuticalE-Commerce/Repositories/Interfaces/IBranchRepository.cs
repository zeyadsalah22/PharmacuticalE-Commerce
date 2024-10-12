using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IBranchRepository
    {
        IEnumerable<Branch> GetAllBranches();
        Branch GetBranchById(int? id);
        void AddBranch(Branch branch);
        void UpdateBranch(Branch branch);
        void DeleteBranch(int id);
        bool BranchExists(int id);
        void Save();
    }
}
