using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface IEmployeeRepository : IRepository<Employee>
	{
		Task<IEnumerable<Employee>> GetAllWithDetails();
		Task<Employee> GetByIdWithDetails(int? id);
		Task<bool> EmployeeExists(int id);
		Task<IEnumerable<Branch>> GetBranches();
		Task<IEnumerable<Role>> GetRoles();
		Task<IEnumerable<Shift>> GetShifts();
		Task<Shift> GetShiftsById(int? id);
		Task UpdateShift(Shift shift);
		Task DeleteShift(int? id);
		Task CreateShift(Shift shift);
	}

}
