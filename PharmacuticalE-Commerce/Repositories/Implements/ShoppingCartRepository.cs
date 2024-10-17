using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class ShoppingCartRepository: IShoppingCartRepository
    {
        private readonly PharmacySystemContext _context;
        private readonly CartRepository _cartRepo;

        public ShoppingCartRepository(PharmacySystemContext context)
        {
            _context = context;
            _cartRepo = new CartRepository(context);
        }

        public void Create(ShoppingCart entity)
        {
            if (_cartRepo.GetById(entity.ShoppingCartId) is null) return;
            if (GetById(entity.ShoppingCartId) is not null) return;

            entity.Status = "Active";

            _context.ShoppingCarts.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _context.ShoppingCarts.ToList();
        }

        public ShoppingCart GetActiveCartOfUser(int? userId)
        {
            return _context.ShoppingCarts.Include(sc => sc.Cart)
                                         .Where(sc => sc.Cart.UserId == userId)
                                         .FirstOrDefault(sc => sc.Status == "Active");
        }

        public ShoppingCart GetById(int? id)
        {
            return _context.ShoppingCarts.FirstOrDefault(sc => sc.ShoppingCartId == id);
        }

        public void Update(ShoppingCart entity)
        {
            var toUpdate = GetById(entity.ShoppingCartId);

            if (toUpdate is null) return;

            toUpdate.Status = entity.Status;
            _context.SaveChanges();
        }
    }
}
