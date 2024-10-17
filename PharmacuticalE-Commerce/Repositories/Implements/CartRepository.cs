using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class CartRepository : ICartRepository
    {
        private readonly PharmacySystemContext _context;
        private readonly UserRepository _userRepo;
        private readonly PrescriptionRepository _prescRepo;
        private readonly ShoppingCartRepository _shoppingRepo;

        public CartRepository(PharmacySystemContext context)
        {
            _context = context;
            _userRepo = new UserRepository(context);
            _prescRepo = new PrescriptionRepository(context);
            _shoppingRepo = new ShoppingCartRepository(context);
        }

        public void Create(Cart entity)
        {
            if (entity.Type is null ||
                _userRepo.GetById(entity.UserId) is null) return;

            _context.Carts.Add(entity);

            if (entity.Type == "Presc")
            {
                var prescToAdd = new Prescription
                {
                    PrescriptionId = entity.CartId,
                    Status = "UnderReview"
                };
                _prescRepo.Create(prescToAdd);
                _context.SaveChanges();
            }
            else if (entity.Type == "Shopping")
            {
                var userShoppingCarts = GetAllByUserIdWithShopping(entity.UserId);
                if (userShoppingCarts.Any(c => c.ShoppingCart.Status == "Active"))
                {
                    _context.Remove(entity);
                    return;
                }

                var shoppingToAdd = new ShoppingCart
                {
                    ShoppingCartId = entity.CartId,
                    Status = "Active"
                };
                _shoppingRepo.Create(shoppingToAdd);

                _context.SaveChanges();
            }
            else _context.Remove(entity);
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cart> GetAll()
        {
            return _context.Carts.ToList();
        }

        public IEnumerable<Cart> GetAllWithShopping()
        {
            return _context.Carts.Include(c => c.ShoppingCart)
                                 .ToList();
        }

        public IEnumerable<Cart> GetAllWithPresc()
        {
            return _context.Carts.Include(c => c.Prescription)
                                 .ToList();
        }
        public IEnumerable<Cart> GetAllByUserIdWithShopping(int? userId)
        {
            return _context.Carts.Include(c => c.ShoppingCart)
                                 .Where(c => c.UserId == userId)
                                 .ToList();
        }
        public IEnumerable<Cart> GetAllByUserIdWithPresc(int? userId)
        {
            return _context.Carts.Include(c => c.Prescription)
                                 .Where(c => c.UserId == userId)
                                 .ToList();
        }

        public Cart GetById(int? id)
        {
            return _context.Carts.FirstOrDefault(c => c.CartId == id);
        }

        public Cart GetByIdWithShopping(int? id)
        {
            return _context.Carts.Include(c => c.ShoppingCart).FirstOrDefault(c => c.CartId == id);
        }

        public Cart GetByIdWithPresc(int? id)
        {
            return _context.Carts.Include(c => c.Prescription).FirstOrDefault(c => c.CartId == id);
        }

        public void Update(Cart entity)
        {
            throw new NotImplementedException();
        }
    }
}
