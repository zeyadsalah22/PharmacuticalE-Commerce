using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly PharmacySystemContext _context;
        private readonly CartRepository _cartRepo;

        public PrescriptionRepository(PharmacySystemContext context)
        {
            _context = context;
            _cartRepo = new CartRepository(context);
        }

        public void Create(Prescription entity)
        {
            if (_cartRepo.GetById(entity.PrescriptionId) is null) return;

            entity.Status = "UnderReview";
            entity.EmployeeId = null;

            _context.Prescriptions.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Prescription> GetAll()
        {
            return _context.Prescriptions.ToList();
        }

        public Prescription GetById(int? id)
        {
            return _context.Prescriptions.FirstOrDefault(p => p.PrescriptionId == id);
        }

        public void Update(Prescription entity)
        {
            var toUpdate = GetById(entity.PrescriptionId);
            
            if (toUpdate is null) return;

            toUpdate.Status = entity.Status;
            _context.SaveChanges();
        }
    }
}
