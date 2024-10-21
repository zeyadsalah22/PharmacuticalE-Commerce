using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories
{
	public interface ICartRepository
	{
		Task<Cart> GetCartByIdAsync(int cartId);
		Task<Cart> GetActiveCartByUserAsync(string userId);
		Task<IEnumerable<Cart>> GetCartsByUserAsync(string userId);
		Task AddCartAsync(Cart cart);
		Task UpdateCartAsync(Cart cart);
		Task DeleteCartAsync(int cartId);
		Task AddCartItemAsync(CartItem cartItem);   // Add cart item
		Task RemoveCartItemAsync(CartItem cartItem);  // Remove cart item
	}
}
