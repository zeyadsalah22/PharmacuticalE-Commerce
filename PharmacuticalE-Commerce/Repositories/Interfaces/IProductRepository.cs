using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        public IEnumerable<Product> GetAllWithCategories();
        public Product GetByIdWithCategories(int? id);

    }
}
