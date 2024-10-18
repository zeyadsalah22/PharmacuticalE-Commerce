using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using PharmacuticalE_Commerce.Migrations;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class OrderRepository : IOrderRepository
    {
        public Guid lifetime { get; set; }
        private readonly PharmacySystemContext _context;
        public OrderRepository(PharmacySystemContext context)
        {
            lifetime = Guid.NewGuid();
            _context = context;
        }
        public void Create(Order entity)
        {
            _context.Orders.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            _context.Orders.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.Include(o=>o.ShippingAddress).ToList();
        }

        public Order GetById(int? id)
        {
            return _context.Orders.Find(id);
        }

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public void Update(Order entity)
        {
            _context.Orders.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Order GetByIdWithDetails(int? Id)
        {
            return _context.Orders.Include(o => o.ShippingAddress)
                .Include(o => o.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(o=>o.OrderId==Id);
        }
    }
}
