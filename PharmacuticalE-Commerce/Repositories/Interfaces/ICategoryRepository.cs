using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Category GetByIdWithParent(int? id);
        public IEnumerable<Category> GetChilds();
        public IEnumerable<Category> GetParents();
        public IEnumerable<Category> GetChildsByparent(int? parentId);
    }
}
