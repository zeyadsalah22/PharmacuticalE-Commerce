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

        public Attendance GetById(int? id)
        {
            return _context.Attendances.Include(a => a.Employee).Include(a => a.Branch)
                    .FirstOrDefault(a => a.RecordId == id);
        }

        public IEnumerable<Attendance> GetAll()
        {
            return _context.Attendances.Include(a => a.Employee).Include(a => a.Branch).ToList();
        }

        public void Create(Attendance entity)
        {
            _context.Attendances.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Attendance entity)
        {
            _context.Attendances.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var attendance = _context.Attendances.Find(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                _context.SaveChanges();
            }
        }

        public IEnumerable<AttendancesViewModel> GetAttendancesByFilter(string shiftId, string branch, DateTime date)
        {
            return _context.Attendances.Include(a => a.Employee).Include(a => a.Branch)
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
                }).ToList();
        }

        public IEnumerable<AttendancesViewModel> GetEmployeesWithoutAttendance(string shiftId, string branch, DateTime date)
        {
            return _context.Employees.Include(e => e.Branch)
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
                }).ToList();
        }

        public bool AttendanceExists(string employeeId, string shiftId, string branchId, DateTime attendedAt)
        {
            return _context.Attendances.Any(a => a.EmployeeId == int.Parse(employeeId) && a.ShiftId == int.Parse(shiftId)
                        && a.BranchId == int.Parse(branchId) && a.AttendedAt.Date == attendedAt.Date);
        }
    }
}
