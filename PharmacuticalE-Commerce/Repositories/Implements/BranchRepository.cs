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

        public IEnumerable<Branch> GetAllBranches()
        {
            return _context.Branches.ToList();
        }

        public Branch GetBranchById(int? id)
        {
            return _context.Branches.Find(id);
        }

        public void AddBranch(Branch branch)
        {
            _context.Add(branch);
        }

        public void UpdateBranch(Branch branch)
        {
            _context.Update(branch);
        }

        public void DeleteBranch(int id)
        {
            var branch = _context.Branches.Find(id);
            if (branch != null)
            {
                _context.Branches.Remove(branch);
            }
        }

        public bool BranchExists(int id)
        {
            return _context.Branches.Any(e => e.BranchId == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
