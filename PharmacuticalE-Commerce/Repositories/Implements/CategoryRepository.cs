using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
	public class CategoryRepository : ICategoryRepository
	{
		public Guid lifetime { get; set; }
		private readonly PharmacySystemContext _context;
		public CategoryRepository(PharmacySystemContext context)
		{
			lifetime = Guid.NewGuid();
			_context = context;
		}

		public async Task Create(Category entity)
		{
			_context.Categories.Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(int? id)
		{
			_context.Categories.Remove(await GetById(id));
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Category>> GetAll()
		{
			return await _context.Categories.ToListAsync();
		}

		public async Task<Category> GetById(int? id)
		{
			return await _context.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);
		}

		public async Task<Category> GetByIdWithParent(int? id)
		{
			return await _context.Categories.Include(c => c.ParentCategory).FirstOrDefaultAsync(p => p.CategoryId == id);
		}

		public async Task Update(Category entity)
		{
			_context.Categories.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Category>> GetChilds()
		{
			return await _context.Categories.Where(p => p.ParentCategoryId != null).ToListAsync();
		}

		public async Task<IEnumerable<Category>> GetParents()
		{
			return await _context.Categories.Where(p => p.ParentCategoryId == null).Include(c => c.InverseParentCategory).ToListAsync();
		}

		public async Task<IEnumerable<Category>> GetChildsByparent(int? parentId)
		{
			return await _context.Categories.Where(p => p.ParentCategoryId == parentId).ToListAsync();
		}

		public async Task<Category> GetCategoryByName(string name)
		{
			return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);

		}
	}
}
