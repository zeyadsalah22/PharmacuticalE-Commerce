using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var shiftIds = _context.Shifts.Select(shift => shift.ShiftId);
            var branch = _context.Branches.Select(branche => branche.Address);
            var attendancesViewModel = new AttendancesViewModel()
            {
                ShiftIds = shiftIds,
                Branch = branch
            };
            return View(attendancesViewModel);
        }

        [HttpPost]
        public IActionResult ShowAttendances(string ShiftId, string Branch, DateTime Date)
        {
            var pharmacySystemContext = _context.Attendances
                .Join(_context.Branches,
                      attendance => attendance.BranchId,
                      branch => branch.BranchId,
                      (attendance, branch) => new { attendance, branch })
                .Join(_context.Employees,
                      ab => new { ab.attendance.EmployeeId, ab.branch.BranchId },
                      employee => new { employee.EmployeeId, employee.BranchId },
                      (ab, employee) => new
                      {
                          ab.attendance,
                          ab.branch,
                          employee
                      })
                .Where(x => x.branch.Address == Branch
                            && x.attendance.ShiftId == int.Parse(ShiftId)
                            && x.attendance.AttendedAt >= Date
                            && x.attendance.AttendedAt < Date.AddDays(1))
                .Select(x => new AttendancesViewModel
                {
                    RecordId = x.attendance.RecordId,
                    FirstName = x.employee.Fname,
                    LastName = x.employee.Lname,
                    BranchAddress = x.branch.Address,
                    ShiftId = x.attendance.ShiftId,
                    AttendedAt = x.attendance.AttendedAt,
                    LeftAt = x.attendance.LeftAt
                })
                .ToList();

            return View(pharmacySystemContext);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacySystemContext = _context.Attendances
                .Join(_context.Employees,
                      attendance => attendance.EmployeeId,
                      employee => employee.EmployeeId,
                      (attendance, employee) => new { attendance, employee })
                .Join(_context.Branches,
                      ae => ae.attendance.BranchId,
                      branch => branch.BranchId,
                      (ae, branch) => new
                      {
                          ae.attendance,
                          ae.employee,
                          branch
                      })
                .Where(x => x.attendance.RecordId == id)
                .Select(x => new AttendancesViewModel
                {
                    EmployeeId =x.attendance.EmployeeId,
                    RecordId = x.attendance.RecordId,
                    FirstName = x.employee.Fname,
                    LastName = x.employee.Lname,
                    BranchAddress = x.branch.Address,
                    ShiftId = x.attendance.ShiftId,
                    AttendedAt = x.attendance.AttendedAt,
                    LeftAt = x.attendance.LeftAt
                })
                .FirstOrDefault();

            if (pharmacySystemContext == null)
            {
                return NotFound();
            }

            return View(pharmacySystemContext);
        }
    }
}
