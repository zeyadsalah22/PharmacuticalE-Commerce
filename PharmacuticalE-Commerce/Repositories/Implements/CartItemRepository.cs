using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly PharmacySystemContext _context;
        private readonly ProductRepository _prodRepo;
        private readonly CartRepository _cartRepo;

        public CartItemRepository(PharmacySystemContext context)
        {
            _context = context;
            _prodRepo = new ProductRepository(context);
            _cartRepo = new CartRepository(context);
        }

        public void Create(CartItem entity)
        {
            var tmp = GetById(entity.CartId, entity.ProductId);

            if (tmp is null)
            {
                _context.CartItems.Add(entity);
                _context.SaveChanges();
            }
            else
            {
                tmp.Quantity += entity.Quantity;
                tmp.IsSelected = entity.IsSelected;
                _context.SaveChanges();
            }
        }

        public IEnumerable<CartItem> GetAll()
        {
            return _context.CartItems.ToList();
        }

        public CartItem GetById(int? cartId, int? prodId)
        {
            return _context.CartItems.SingleOrDefault(ci => ci.CartId == cartId && ci.ProductId == prodId);
        }

        public void Update(CartItem entity)
        {
            var cart = _cartRepo.GetById(entity.CartId);

            if (cart is null || cart.Type != "Shopping") return;
            if (_prodRepo.GetById(entity.ProductId) is null) return;

            var tmp = GetById(entity.CartId, entity.ProductId);
            if (tmp is null) return;

            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
