using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
	public class ProductRepository : IProductRepository
	{
		public Guid lifetime { get; set; }
		private readonly PharmacySystemContext _context;
		public ProductRepository(PharmacySystemContext context)
		{
			lifetime = Guid.NewGuid();
			_context = context;
		}

		public async Task Create(Product entity)
		{
			_context.Products.Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(int? id)
		{
			_context.Products.Remove(await GetById(id));
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Product>> GetAll()
		{
			return await _context.Products.Include(p => p.Discount).ToListAsync();
		}

		public async Task<Product> GetById(int? id)
		{
			return await _context.Products.Include(p => p.Discount).FirstOrDefaultAsync(p => p.ProductId == id);
		}

		public async Task<IEnumerable<Product>> GetAllWithCategories()
		{
			return await _context.Products.Include(p => p.Discount).Include(p => p.Category).Include(p => p.Category.ParentCategory).ToListAsync();
		}

		public async Task<Product> GetByIdWithCategories(int? id)
		{
			return await _context.Products.Include(p => p.Discount).Include(p => p.Category).Include(p => p.Category.ParentCategory).FirstOrDefaultAsync(m => m.ProductId == id);
		}

		public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
		{
			return await _context.Products.Include(p => p.Discount).Include(p => p.Category).Where(p => p.CategoryId == categoryId).ToListAsync();
		}

		public async Task Update(Product entity)
		{
			_context.Products.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
