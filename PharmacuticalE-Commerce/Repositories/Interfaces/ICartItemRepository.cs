using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface ICartItemRepository
	{
		Task<CartItem> GetById(int? cartId, int? prodId);

		Task<IEnumerable<CartItem>> GetAll();

		Task Create(CartItem entity);

		Task Update(CartItem entity);
	}
}
