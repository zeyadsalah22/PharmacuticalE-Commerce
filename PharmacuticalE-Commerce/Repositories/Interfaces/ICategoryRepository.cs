using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public IEnumerable<Category> GetChilds();
        public IEnumerable<Category> GetParents();
        public IEnumerable<Category> GetChildsByparent(int? parentId);
    }
}
