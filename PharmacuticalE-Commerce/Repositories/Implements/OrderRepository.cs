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
		public async Task Create(Order entity)
		{
			_context.Orders.Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(int? id)
		{
			_context.Orders.Remove(await GetById(id));
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Order>> GetAll()
		{
			return await _context.Orders.Include(o => o.ShippingAddress).ToListAsync();
		}

		public async Task<Order> GetById(int? id)
		{
			return await _context.Orders.FindAsync(id);
		}

		public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
		{
			return await _context.Orders.Include(o => o.Cart).ThenInclude(oc => oc.CartItems).Where(o => o.UserId == userId).ToListAsync();
		}

		public async Task Update(Order entity)
		{
			_context.Orders.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task<Order> GetByIdWithDetails(int? Id)
		{
			return await _context.Orders.Include(o => o.ShippingAddress)
				.Include(o => o.Cart)
				.ThenInclude(c => c.CartItems)
				.ThenInclude(ci => ci.Product)
				.ThenInclude(p => p.Discount)
				.FirstOrDefaultAsync(o => o.OrderId == Id);
		}
	}
}
