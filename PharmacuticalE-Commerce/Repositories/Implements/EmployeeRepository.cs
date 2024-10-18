using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PharmacySystemContext _context;

        public EmployeeRepository(PharmacySystemContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAllWithDetails()
        {
            return _context.Employees
                           .Include(e => e.Branch)
                           .Include(e => e.Role)
                           .Include(e => e.Shift)
                           .ToList();
        }

        public Employee GetByIdWithDetails(int? id)
        {
            return _context.Employees
                           .Include(e => e.Branch)
                           .Include(e => e.Role)
                           .Include(e => e.Shift)
                           .FirstOrDefault(e => e.EmployeeId == id);
        }

        public bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        public IEnumerable<Branch> GetBranches()
        {
            return _context.Branches.ToList();
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public IEnumerable<Shift> GetShifts()
        {
            return _context.Shifts.ToList();
        }

        public Employee GetById(int? id)
        {
            return _context.Employees.Find(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public void Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public void UpdateShift(Shift shift)
        {
            _context.Shifts.Update(shift);
            _context.SaveChanges();
        }

        public Shift GetShiftsById(int? id)
        {
            return _context.Shifts.Find(id);
        }

        public void CreateShift(Shift shift)
        {
            _context.Shifts.Add(shift);
            _context.SaveChanges();
        }

        public void DeleteShift(int? id)
        {
            var shift = _context.Shifts.Find(id);
            if (shift != null)
            {
                _context.Shifts.Remove(shift);
                _context.SaveChanges();
            }
        }
    }
}
