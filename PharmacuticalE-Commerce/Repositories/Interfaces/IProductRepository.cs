using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<IEnumerable<Product>> GetProductsByCategory(int categoryId);
		Task<IEnumerable<Product>> GetAllWithCategories();
		Task<Product> GetByIdWithCategories(int? id);
	}
}
