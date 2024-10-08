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
            var pharmacySystemContext = _context.Attendances.Include(a => a.Branch).Include(a => a.Employee)
            .Where(a => a.Branch.Address == Branch&& a.ShiftId == int.Parse(ShiftId)&& a.AttendedAt >= Date&& a.AttendedAt < Date.AddDays(1)).Select(a => new AttendancesViewModel
                {
                    RecordId = a.RecordId,
                    FirstName = a.Employee.Fname,
                    LastName = a.Employee.Lname,
                    BranchAddress = a.Branch.Address,
                    ShiftId = a.ShiftId,
                    AttendedAt = a.AttendedAt,
                    LeftAt = a.LeftAt
                }).ToList();

            return View(pharmacySystemContext);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacySystemContext = _context.Attendances.Include(a => a.Employee).Include(a => a.Branch).Where(a => a.RecordId == id).Select(a => new AttendancesViewModel
                {
                    EmployeeId = a.EmployeeId,
                    RecordId = a.RecordId,
                    FirstName = a.Employee.Fname,
                    LastName = a.Employee.Lname,
                    BranchAddress = a.Branch.Address,
                    ShiftId = a.ShiftId,
                    AttendedAt = a.AttendedAt,
                    LeftAt = a.LeftAt
                }).FirstOrDefault();

            if (pharmacySystemContext == null)
            {
                return NotFound();
            }

            return View(pharmacySystemContext);
        }
    }
}
