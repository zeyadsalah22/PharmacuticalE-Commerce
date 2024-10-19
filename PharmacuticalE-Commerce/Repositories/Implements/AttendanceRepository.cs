using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using PharmacuticalE_Commerce.ViewModels;

namespace PharmacuticalE_Commerce.Repositories.Implements
{
	public class AttendanceRepository : IAttendanceRepository
	{
		private readonly PharmacySystemContext _context;

		public AttendanceRepository(PharmacySystemContext context)
		{
			_context = context;
		}

		public async Task<Attendance> GetById(int? id)
		{
			return await _context.Attendances.Include(a => a.Employee).Include(a => a.Branch).FirstOrDefaultAsync(a => a.RecordId == id);
		}

		public async Task<IEnumerable<Attendance>> GetAll()
		{
			return await _context.Attendances.Include(a => a.Employee).Include(a => a.Branch).ToListAsync();
		}

		public async Task Create(Attendance entity)
		{
			_context.Attendances.Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Update(Attendance entity)
		{
			_context.Attendances.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(int? id)
		{
			var attendance = await _context.Attendances.FindAsync(id);
			if (attendance != null)
			{
				_context.Attendances.Remove(attendance);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<AttendancesViewModel>> GetAttendancesByFilter(string shiftId, string branch, DateTime date)
		{
			return await _context.Attendances.Include(a => a.Employee).Include(a => a.Branch)
				.Where(a => a.Branch.Address == branch && a.ShiftId == int.Parse(shiftId)
						&& a.AttendedAt >= date && a.AttendedAt < date.AddDays(1))
				.Select(a => new AttendancesViewModel
				{
					RecordId = a.RecordId,
					FirstName = a.Employee.Fname,
					LastName = a.Employee.Lname,
					BranchAddress = a.Branch.Address,
					ShiftId = a.ShiftId,
					AttendedAt = a.AttendedAt,
					LeftAt = a.LeftAt
				}).ToListAsync();
		}

		public async Task<IEnumerable<AttendancesViewModel>> GetEmployeesWithoutAttendance(string shiftId, string branch, DateTime date)
		{
			return await _context.Employees.Include(e => e.Branch)
				.Where(e => e.ShiftId == int.Parse(shiftId) && e.Branch.Address == branch
						&& !_context.Attendances.Any(a => a.ShiftId == int.Parse(shiftId) && a.AttendedAt >= date
								&& a.AttendedAt < date.AddDays(1) && a.EmployeeId == e.EmployeeId))
				.Select(e => new AttendancesViewModel
				{
					EmployeeId = e.EmployeeId,
					FirstName = e.Fname,
					LastName = e.Lname,
					BranchAddress = e.Branch.Address,
					BranchId = e.BranchId,
					ShiftId = e.ShiftId
				}).ToListAsync();
		}

		public async Task<bool> AttendanceExists(string employeeId, string shiftId, string branchId, DateTime attendedAt)
		{
			return await _context.Attendances.AnyAsync(a => a.EmployeeId == int.Parse(employeeId) && a.ShiftId == int.Parse(shiftId)
						&& a.BranchId == int.Parse(branchId) && a.AttendedAt.Date == attendedAt.Date);
		}

		public async Task<IEnumerable<string>> GetAllBranches()
		{
			return await _context.Branches.Select(a => a.Address).ToListAsync();
		}

		public async Task<IEnumerable<int>> GetAllShifts()
		{
			return await _context.Shifts.Select(a => a.ShiftId).ToListAsync();
		}

		public async Task<string> GetBranchAddress(int id)
		{
			return await _context.Branches.Where(a => a.BranchId == id).Select(a => a.Address).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Shift>> GetallShifts()
		{
			return await _context.Shifts.ToListAsync();
		}
	}
}
