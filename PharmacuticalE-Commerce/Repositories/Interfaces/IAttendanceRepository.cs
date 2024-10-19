using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.ViewModels;
using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface IAttendanceRepository : IRepository<Attendance>
	{
		Task<IEnumerable<AttendancesViewModel>> GetAttendancesByFilter(string shiftId, string branch, DateTime date);
		Task<IEnumerable<AttendancesViewModel>> GetEmployeesWithoutAttendance(string shiftId, string branch, DateTime date);
		Task<IEnumerable<string>> GetAllBranches();
		Task<IEnumerable<int>> GetAllShifts();
		Task<IEnumerable<Shift>> GetallShifts();
		Task<string> GetBranchAddress(int id);
		Task<bool> AttendanceExists(string employeeId, string shiftId, string branchId, DateTime attendedAt);
	}
}
