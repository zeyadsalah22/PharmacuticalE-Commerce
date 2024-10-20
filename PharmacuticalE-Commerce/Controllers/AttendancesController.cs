﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using PharmacuticalE_Commerce.ViewModels;

namespace PharmacuticalE_Commerce.Controllers
{
	[Authorize(Roles = "Admin,HR")]
	public class AttendancesController : Controller
	{
		private readonly IAttendanceRepository _attendanceRepository;

		public AttendancesController(IAttendanceRepository attendanceRepository)
		{
			_attendanceRepository = attendanceRepository;
		}

		public async Task<IActionResult> Index()
		{
			var attendancesViewModel = new AttendancesViewModel
			{
				Shifts = await _attendanceRepository.GetallShifts(),
				Branch = await _attendanceRepository.GetAllBranches()
			};

			return View(attendancesViewModel);
		}

		[HttpGet]
		public async Task<IActionResult> ShowAttendances(string ShiftId, string Branch, DateTime Date)
		{
			var attendancesViewModel = await _attendanceRepository.GetAttendancesByFilter(ShiftId, Branch, Date);
			return View(attendancesViewModel);
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var attendance = await _attendanceRepository.GetById(id);

			if (attendance == null)
				return NotFound();

			var attendanceViewModel = new AttendancesViewModel
			{
				EmployeeId = attendance.EmployeeId,
				FirstName = attendance.Employee.Fname,
				LastName = attendance.Employee.Lname,
				BranchAddress = attendance.Branch.Address,
				ShiftId = attendance.ShiftId,
				AttendedAt = attendance.AttendedAt,
				LeftAt = attendance.LeftAt
			};

			return View(attendanceViewModel);
		}

		[HttpGet]
		public async Task<IActionResult> TakeAttendance(string shiftId, string branch, DateTime Date)
		{
			var employees = (await _attendanceRepository.GetEmployeesWithoutAttendance(shiftId, branch, Date)).Select(a => new AttendancesViewModel
			{
				EmployeeId = a.EmployeeId,
				FirstName = a.FirstName,
				LastName = a.LastName,
				ShiftId = a.ShiftId,
				BranchAddress = branch,
				AttendedAt = a.AttendedAt,
				BranchId = a.BranchId,
			}
			).ToList();
			return View(employees);
		}

		[HttpGet]
		public IActionResult Attended(string employeeId, string shiftId, string branchId)
		{

			var attendance = new Attendance
			{
				EmployeeId = int.Parse(employeeId),
				ShiftId = int.Parse(shiftId),
				BranchId = int.Parse(branchId)
			};

			return View(attendance);
		}

		[HttpPost]
		public async Task<IActionResult> tAttended(AttendancesViewModel attendance)
		{
			var emp = new Attendance
			{
				EmployeeId = attendance.EmployeeId,
				ShiftId = attendance.ShiftId,
				BranchId = attendance.BranchId,
				AttendedAt = attendance.AttendedAt,
				LeftAt = attendance.LeftAt,
			};
			if (await _attendanceRepository.AttendanceExists(emp.EmployeeId.ToString(), emp.ShiftId.ToString(), emp.BranchId.ToString(), emp.AttendedAt))
			{
				TempData["Error"] = "Attendance for this employee has already been recorded for today.";
				return View("Attended", emp);
			}

			await _attendanceRepository.Create(emp);
			return RedirectToAction("TakeAttendance", new
			{
				shiftId = attendance.ShiftId.ToString(),
				Branch = await _attendanceRepository.GetBranchAddress(attendance.BranchId),
				Date = attendance.AttendedAt.ToString("yyyy-MM-dd")

			}
			);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var attendance = await _attendanceRepository.GetById(id);
			if (attendance == null)
			{
				return NotFound();
			}

			var viewModel = new AttendancesViewModel
			{
				RecordId = attendance.RecordId,
				EmployeeId = attendance.EmployeeId,
				ShiftId = attendance.ShiftId,
				BranchId = attendance.BranchId,
				AttendedAt = attendance.AttendedAt,
				LeftAt = attendance.LeftAt,
				BranchAddress = await _attendanceRepository.GetBranchAddress(attendance.BranchId),
				ShiftIds = await _attendanceRepository.GetAllShifts(),
				Branch = await _attendanceRepository.GetAllBranches()
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, AttendancesViewModel viewModel)
		{
			if (id != viewModel.RecordId)
				return NotFound();


			var attendance = await _attendanceRepository.GetById(id);

			if (attendance == null)
				return NotFound();

			attendance.AttendedAt = viewModel.AttendedAt;
			attendance.LeftAt = viewModel.LeftAt;

			await _attendanceRepository.Update(attendance);

			return RedirectToAction(nameof(ShowAttendances), new
			{
				ShiftId = attendance.ShiftId.ToString(),
				Branch = attendance.Branch.Address,
				Date = attendance.AttendedAt.Date.ToString("yyyy-MM-dd")
			});

		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var attendance = await _attendanceRepository.GetById(id);

			if (attendance == null)
				return NotFound();

			var attendanceViewModel = new AttendancesViewModel
			{
				EmployeeId = attendance.EmployeeId,
				FirstName = attendance.Employee.Fname,
				LastName = attendance.Employee.Lname,
				BranchAddress = attendance.Branch.Address,
				ShiftId = attendance.ShiftId,
				AttendedAt = attendance.AttendedAt,
				LeftAt = attendance.LeftAt
			};

			return View(attendanceViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var attendance = await _attendanceRepository.GetById(id);

			if (attendance == null)
				return NotFound();

			var shiftId = attendance.ShiftId;
			var branch = attendance.Branch.Address;
			var date = attendance.AttendedAt.Date;

			await _attendanceRepository.Delete(id);

			return RedirectToAction("ShowAttendances", new
			{
				ShiftId = shiftId.ToString(),
				Branch = branch,
				Date = date.ToString("yyyy-MM-dd")
			});
		}
	}

}