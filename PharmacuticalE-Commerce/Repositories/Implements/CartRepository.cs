using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacuticalE_Commerce.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly PharmacySystemContext _context;

        public CartRepository(PharmacySystemContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            return await _context.Carts
                                 .Include(c => c.CartItems)
                                 .ThenInclude(ci => ci.Product)
                                 .ThenInclude(p => p.Discount)
                                 .FirstOrDefaultAsync(c => c.CartId == cartId);
        }

        public async Task<Cart> GetActiveCartByUserAsync(string userId)
        {
            return await _context.Carts
                                 .Include(c => c.CartItems)
                                 .ThenInclude(ci => ci.Product)
                                 .ThenInclude(p => p.Discount)
                                 .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == true);  // Only return active cart
        }

        public async Task<IEnumerable<Cart>> GetCartsByUserAsync(string userId)
        {
            return await _context.Carts
                                 .Include(c => c.CartItems)
                                 .Where(c => c.UserId == userId)
                                 .ToListAsync();
        }

        public async Task AddCartAsync(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Carts.Attach(cart);
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(int cartId)
        {
            var cart = await GetCartByIdAsync(cartId);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
