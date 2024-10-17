using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetAllWithDetails();
        Employee GetByIdWithDetails(int? id);
        bool EmployeeExists(int id);
        IEnumerable<Branch> GetBranches();
        IEnumerable<Role> GetRoles();
        IEnumerable<Shift> GetShifts();
        Shift GetShiftsById(int? id);
        void UpdateShift(Shift shift);
    }

}
