using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.ViewModels;

namespace PharmacuticalE_Commerce.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly PharmacySystemContext _context;

        public AttendancesController(PharmacySystemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var attendancesViewModel = new AttendancesViewModel
            {
                ShiftIds = _context.Shifts.Select(shift => shift.ShiftId),
                Branch = _context.Branches.Select(branch => branch.Address)
            };

            return View(attendancesViewModel);
        }

        [HttpPost]
        public IActionResult ShowAttendances(string ShiftId, string Branch, DateTime Date)
        {
            var attendancesViewModel = _context.Attendances.Include(a => a.Branch).Include(a => a.Employee).Where(a => a.Branch.Address == Branch &&a.ShiftId == int.Parse(ShiftId) &&a.AttendedAt >= Date &&a.AttendedAt < Date.AddDays(1))
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

            return View(attendancesViewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var attendanceViewModel = _context.Attendances.Include(a => a.Employee).Include(a => a.Branch).Where(a => a.RecordId == id)
                .Select(a => new AttendancesViewModel
                {
                    EmployeeId = a.EmployeeId,
                    RecordId = a.RecordId,
                    FirstName = a.Employee.Fname,
                    LastName = a.Employee.Lname,
                    BranchAddress = a.Branch.Address,
                    ShiftId = a.ShiftId,
                    AttendedAt = a.AttendedAt,
                    LeftAt = a.LeftAt
                })
                .FirstOrDefault();

            return attendanceViewModel == null ? NotFound() : View(attendanceViewModel);
        }

        public IActionResult TakeAttendance(string shiftId, string Branch, DateTime Date)
        {
            var employees = _context.EmployeeShifts.Include(es => es.Employee).Include(es => es.Employee.Branch).Where(es => es.ShiftId == int.Parse(shiftId) &&es.Employee.Branch.Address == Branch &&!_context.Attendances.Any(a => a.ShiftId == int.Parse(shiftId) &&a.AttendedAt >= Date &&a.LeftAt < Date.AddDays(1) &&a.EmployeeId == es.EmployeeId))
            .Select(es => new AttendancesViewModel
            {
                EmployeeId = es.Employee.EmployeeId,
                FirstName = es.Employee.Fname,
                LastName = es.Employee.Lname,
                BranchAddress = es.Employee.Branch.Address,
                BranchId = es.Employee.Branch.BranchId,
                ShiftId = es.ShiftId
            });

            return View(employees.ToList());
        }

        public IActionResult Attended(string employeeId, string shiftId, string branchId)
        {
            ViewBag.EmployeeId = employeeId;
            ViewBag.ShiftId = shiftId;
            ViewBag.BranchId = branchId;

            return View();
        }

        [HttpPost]
        public IActionResult tAttended(Attendance attendance)
        {
            if (_context.Attendances.Any(a => a.EmployeeId == attendance.EmployeeId &&a.ShiftId == attendance.ShiftId &&a.BranchId == attendance.BranchId &&a.AttendedAt.Date == attendance.AttendedAt.Date))
            {
                TempData["Error"] = "Attendance for this employee has already been recorded for today.";
                ViewBag.EmployeeId = attendance.EmployeeId;
                ViewBag.ShiftId = attendance.ShiftId;
                ViewBag.BranchId = attendance.BranchId;

                return View("Attended", attendance);
            }

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
