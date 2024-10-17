using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PharmacySystemContext _context;

        public RoleRepository(PharmacySystemContext context)
        {
            _context = context;
        }

        public Role GetById(int? id)
        {
            return _context.Roles.FirstOrDefault(r => r.RoleId == id);
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public void Create(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var role = GetById(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }
    }
}
