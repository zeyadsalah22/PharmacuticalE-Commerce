using Microsoft.AspNetCore.Mvc;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using PharmacuticalE_Commerce.ViewModels;

namespace PharmacuticalE_Commerce.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendancesController(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public IActionResult Index()
        {
            var attendancesViewModel = new AttendancesViewModel
            {
                ShiftIds = _attendanceRepository.GetAll().Select(a => a.ShiftId),
                Branch = _attendanceRepository.GetAll().Select(a => a.Branch.Address)
            };

            return View(attendancesViewModel);
        }

        [HttpPost]
        public IActionResult ShowAttendances(string ShiftId, string Branch, DateTime Date)
        {
            var attendancesViewModel = _attendanceRepository.GetAttendancesByFilter(ShiftId, Branch, Date);
            return View(attendancesViewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            // Get the attendance record by id from the repository
            var attendance = _attendanceRepository.GetById(id);

            // Check if the attendance exists
            if (attendance == null)
                return NotFound();

            // Map Attendance model to AttendancesViewModel
            var attendanceViewModel = new AttendancesViewModel
            {
                EmployeeId = attendance.EmployeeId,
                FirstName = attendance.Employee.Fname, // Assuming Employee object is included in Attendance
                LastName = attendance.Employee.Lname,
                BranchAddress = attendance.Branch.Address,
                ShiftId = attendance.ShiftId,
                AttendedAt = attendance.AttendedAt,
                LeftAt = attendance.LeftAt
            };

            // Return the ViewModel to the view
            return View(attendanceViewModel);
        }


        public IActionResult TakeAttendance(string shiftId, string Branch, DateTime Date)
        {
            var employees = _attendanceRepository.GetEmployeesWithoutAttendance(shiftId, Branch, Date);
            return View(employees.ToList());
        }

        public IActionResult Attended(string employeeId, string shiftId, string branchId)
        {
            // Create attendance model with employee, shift, and branch details.
            var attendance = new Attendance
            {
                EmployeeId = int.Parse(employeeId),
                ShiftId = int.Parse(shiftId),
                BranchId = int.Parse(branchId)
            };

            return View(attendance);
        }

        [HttpPost]
        public IActionResult tAttended(Attendance attendance)
        {
            // Check if attendance already exists for this employee, shift, and date.
            if (_attendanceRepository.AttendanceExists(attendance.EmployeeId.ToString(), attendance.ShiftId.ToString(), attendance.BranchId.ToString(), attendance.AttendedAt))
            {
                TempData["Error"] = "Attendance for this employee has already been recorded for today.";
                return View("Attended", attendance);
            }

            // Create new attendance record.
            _attendanceRepository.Create(attendance);
            return RedirectToAction("Index");
        }
    }
}
