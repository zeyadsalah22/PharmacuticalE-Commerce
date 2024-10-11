using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class PAVRepository : IPAVRepository
    {
        public Guid lifetime { get; set; }
        private readonly PharmacySystemContext _context;
        public PAVRepository(PharmacySystemContext context)
        {
            lifetime = Guid.NewGuid();
            _context = context;
        }
        public void Create(Pav entity)
        {
            _context.Pavs.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            _context.Pavs.Remove(GetById(id));
            _context.SaveChanges();
        }

        public void DeleteByCompId(int? productId, int? attributeId)
        {
            _context.Pavs.Remove(GetByCompId(productId, attributeId));
            _context.SaveChanges();
        }

        public IEnumerable<Pav> GetAll()
        {
            return _context.Pavs.ToList();
        }

        public Pav GetByCompId(int? productId, int? attributeId)
        {
            return _context.Pavs.FirstOrDefault(p => p.ProductId == productId && p.AttributeId == attributeId);
        }

        public Pav GetById(int? id)
        {
            return _context.Pavs.FirstOrDefault(p => p.ProductId == id);
        }

        public void Update(Pav entity)
        {
            _context.Pavs.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
