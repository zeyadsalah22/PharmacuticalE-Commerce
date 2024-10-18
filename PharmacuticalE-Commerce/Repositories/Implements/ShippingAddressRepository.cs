using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class ShippingAddressRepository : IShippingAddressRepository
    {
        public Guid lifetime { get; set; }
        private readonly PharmacySystemContext _context;
        public ShippingAddressRepository(PharmacySystemContext context)
        {
            lifetime = Guid.NewGuid();
            _context = context;
        }
        public void Create(ShippingAddress entity)
        {
            _context.ShippingAddresses.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var shippingAddress = GetById(id);
            shippingAddress.IsDeleted = true;
            _context.ShippingAddresses.Attach(shippingAddress);
            _context.Entry(shippingAddress).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<ShippingAddress> GetAll()
        {
            return _context.ShippingAddresses.ToList();
        }

        public ShippingAddress GetById(int? id)
        {
            return _context.ShippingAddresses.FirstOrDefault(s => s.AddressId == id);
        }

        public void Update(ShippingAddress entity)
        {
            _context.ShippingAddresses.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<ShippingAddress> GetShippingAddressByUserId(string userId)
        {
            return _context.ShippingAddresses.Where(s => s.UserId == userId && s.IsDeleted==false).ToList();
        }
    }
}
