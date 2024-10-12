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
        public void Create(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            _context.Categories.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int? id)
        {
            return _context.Categories.FirstOrDefault(p => p.CategoryId == id);
        }

        public void Update(Category entity)
        {
            _context.Categories.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetChilds()
        {
            return _context.Categories.Where(p => p.ParentCategoryId != null).ToList();
        }

        public IEnumerable<Category> GetParents()
        {
            return _context.Categories.Where(p => p.ParentCategoryId == null).Include(c => c.InverseParentCategory).ToList();
        }

        public IEnumerable<Category> GetChildsByparent(int? parentId)
        {
            return _context.Categories.Where(p => p.ParentCategoryId == parentId).ToList();
        }
    }
}
