using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly PharmacySystemContext _context;

        public CartItemRepository(PharmacySystemContext context)
        {
            _context = context;
        }
        public async Task Create(CartItem entity)
        {
            _context.CartItems.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var cartItem = await GetById(id);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem>> GetAll()
        {
            return _context.CartItems.ToList();
        }

        public async Task<CartItem> GetById(int? id)
        {
            return _context.CartItems.FirstOrDefault(x => x.CartId == id);
        }

        public async Task Update(CartItem entity)
        {
            _context.CartItems.Attach(entity);
            _context.CartItems.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
