using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface ICartItemRepository : IRepository<CartItem>
	{
		Task<IEnumerable<CartItem>> GetCartItemsByProductId(int productId);
	}
}
