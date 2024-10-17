using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class UserRepository: IUserRepository
    {
        private readonly PharmacySystemContext _context;

        public UserRepository(PharmacySystemContext context)
        {
            _context = context;
        }

        public void Create(User entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int? id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public void Update(User entity)
        {
            _context.Users.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
