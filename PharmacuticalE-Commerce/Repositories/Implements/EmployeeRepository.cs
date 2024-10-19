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

		public async Task<IEnumerable<Employee>> GetAllWithDetails()
		{
			return await _context.Employees
				.Include(e => e.Branch)
				.Include(e => e.Role)
				.Include(e => e.Shift)
				.ToListAsync();
		}

		public async Task<Employee> GetByIdWithDetails(int? id)
		{
			return await _context.Employees
				.Include(e => e.Branch)
				.Include(e => e.Role)
				.Include(e => e.Shift)
				.FirstOrDefaultAsync(e => e.EmployeeId == id);
		}

		public async Task<bool> EmployeeExists(int id)
		{
			return await _context.Employees.AnyAsync(e => e.EmployeeId == id);
		}

		public async Task<IEnumerable<Branch>> GetBranches()
		{
			return await _context.Branches.ToListAsync();
		}

		public async Task<IEnumerable<Role>> GetRoles()
		{
			return await _context.Roles.ToListAsync();
		}

		public async Task<IEnumerable<Shift>> GetShifts()
		{
			return await _context.Shifts.ToListAsync();
		}

		public async Task<Employee> GetById(int? id)
		{
			return await _context.Employees.FindAsync(id);
		}

		public async Task<IEnumerable<Employee>> GetAll()
		{
			return await _context.Employees.ToListAsync();
		}

		public async Task Create(Employee employee)
		{
			_context.Employees.Add(employee);
			await _context.SaveChangesAsync();
		}

		public async Task Update(Employee employee)
		{
			_context.Employees.Update(employee);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(int? id)
		{
			var employee = await _context.Employees.FindAsync(id);
			if (employee != null)
			{
				_context.Employees.Remove(employee);
				await _context.SaveChangesAsync();
			}
		}

		public async Task UpdateShift(Shift shift)
		{
			_context.Shifts.Update(shift);
			await _context.SaveChangesAsync();
		}

		public async Task<Shift> GetShiftsById(int? id)
		{
			return await _context.Shifts.FindAsync(id);
		}

		public async Task CreateShift(Shift shift)
		{
			_context.Shifts.Add(shift);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteShift(int? id)
		{
			var shift = await _context.Shifts.FindAsync(id);
			if (shift != null)
			{
				_context.Shifts.Remove(shift);
				await _context.SaveChangesAsync();
			}
		}
	}
}
