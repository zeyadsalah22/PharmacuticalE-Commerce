using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.ViewModels;
using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        IEnumerable<AttendancesViewModel> GetAttendancesByFilter(string shiftId, string branch, DateTime date);
        IEnumerable<AttendancesViewModel> GetEmployeesWithoutAttendance(string shiftId, string branch, DateTime date);
        IEnumerable<string> GetAllBranches();
        IEnumerable<int> GetAllShifts();
        IEnumerable<Shift> GetallShifts();
        public string GetBranchAddress(int id);
        bool AttendanceExists(string employeeId, string shiftId, string branchId, DateTime attendedAt);
    }
}
