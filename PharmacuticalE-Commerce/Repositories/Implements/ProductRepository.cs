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
        public void Create(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            _context.Products.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int? id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public IEnumerable<Product> GetAllWithCategories()
        {
            return _context.Products.Include(p=>p.Category).Include(p=>p.Category.ParentCategory).ToList();
        }

        public Product GetByIdWithCategories(int? id)
        {
            return _context.Products.Include(p=>p.Category).Include(p => p.Category.ParentCategory).FirstOrDefault(m => m.ProductId == id);
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Include(p => p.Category).Where(p=>p.CategoryId==categoryId);
        }

        public void Update(Product entity)
        {
            _context.Products.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
