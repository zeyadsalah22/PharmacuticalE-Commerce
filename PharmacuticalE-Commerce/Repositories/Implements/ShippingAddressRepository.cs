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

		public async Task Create(ShippingAddress entity)
		{
			_context.ShippingAddresses.Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(int? id)
		{
			var shippingAddress = await GetById(id);
			shippingAddress.IsDeleted = true;
			_context.ShippingAddresses.Attach(shippingAddress);
			_context.Entry(shippingAddress).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<ShippingAddress>> GetAll()
		{
			return await _context.ShippingAddresses.ToListAsync();
		}

		public async Task<ShippingAddress> GetById(int? id)
		{
			return await _context.ShippingAddresses.FirstOrDefaultAsync(s => s.AddressId == id);
		}

		public async Task Update(ShippingAddress entity)
		{
			_context.ShippingAddresses.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<ShippingAddress>> GetShippingAddressByUserId(string userId)
		{
			return await _context.ShippingAddresses.Where(s => s.UserId == userId && s.IsDeleted == false).ToListAsync();
		}
	}
}
