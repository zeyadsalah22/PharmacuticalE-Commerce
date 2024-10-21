using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
	public class DiscountRepository : IDiscountRepository
	{
		public Guid lifetime { get; set; }
		private readonly PharmacySystemContext _context;

		public DiscountRepository(PharmacySystemContext context)
		{
			lifetime = Guid.NewGuid();
			_context = context;
		}

		public async Task Create(Discount entity)
		{
			_context.Discounts.Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(int? id)
		{
			_context.Discounts.Remove(await GetById(id));
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Discount>> GetAll()
		{
			return await _context.Discounts.ToListAsync();
		}

		public async Task<Discount> GetById(int? id)
		{
			return await _context.Discounts.FirstOrDefaultAsync(d => d.DiscountId == id);
		}

		public async Task<Discount> GetByIdWithProduct(int? id)
		{
			return await _context.Discounts
								 .Include(d => d.Product)
								 .FirstOrDefaultAsync(d => d.DiscountId == id);
		}

		public async Task Update(Discount entity)
		{
			_context.Discounts.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
