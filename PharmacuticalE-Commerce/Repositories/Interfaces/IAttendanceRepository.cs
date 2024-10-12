using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.ViewModels;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        IEnumerable<AttendancesViewModel> GetAttendancesByFilter(string shiftId, string branch, DateTime date);
        IEnumerable<AttendancesViewModel> GetEmployeesWithoutAttendance(string shiftId, string branch, DateTime date);
        bool AttendanceExists(string employeeId, string shiftId, string branchId, DateTime attendedAt);
    }
}
