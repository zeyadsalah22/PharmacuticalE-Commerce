using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        public Guid lifetime { get; set; }
        private readonly PharmacySystemContext _context;
        public ProductAttributeRepository(PharmacySystemContext context)
        {
            lifetime = Guid.NewGuid();
            _context = context;
        }
        public void Create(ProductAttribute entity)
        {
            _context.ProductAttributes.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            _context.ProductAttributes.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<ProductAttribute> GetAll()
        {
            return _context.ProductAttributes.ToList();
        }

        public ProductAttribute GetById(int? id)
        {
            return _context.ProductAttributes.FirstOrDefault(p => p.AttributeId == id);
        }

        public void Update(ProductAttribute entity)
        {
            _context.ProductAttributes.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
