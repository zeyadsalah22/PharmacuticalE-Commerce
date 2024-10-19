using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using PharmacuticalE_Commerce.ViewModels;

namespace PharmacuticalE_Commerce.Controllers
{
    [Authorize(Roles ="Admin,HR")]
	public class EmployeesController : Controller
	{
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeesController(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<IActionResult> Index()
		{
			var employees = await _employeeRepository.GetAllWithDetails();
			return View(employees);
		}

		public async Task<IActionResult> Details(int? id)
		{
			var employee = await _employeeRepository.GetByIdWithDetails(id);
			return View(employee);
		}

		public async Task<IActionResult> Create()
		{
			var viewModel = new EmployeeViewModel
			{
				Employee = new Employee(),
				Branches = await _employeeRepository.GetBranches(),
				Roles = await _employeeRepository.GetRoles(),
				Shifts = await _employeeRepository.GetShifts()
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(EmployeeViewModel viewModel)
		{
			await _employeeRepository.Create(viewModel.Employee);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int? id)
		{
			var employee = await _employeeRepository.GetById(id);

			var viewModel = new EmployeeViewModel
			{
				Employee = employee,
				Branches = await _employeeRepository.GetBranches(),
				Roles = await _employeeRepository.GetRoles(),
				Shifts = await _employeeRepository.GetShifts()
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, EmployeeViewModel viewModel)
		{
			await _employeeRepository.Update(viewModel.Employee);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id)
		{
			var employee = await _employeeRepository.GetByIdWithDetails(id);
			return View(employee);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _employeeRepository.Delete(id);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Shifts()
		{
			return View(await _employeeRepository.GetShifts());
		}

		public async Task<IActionResult> ShiftEdit(int? id)
		{
			var shift = await _employeeRepository.GetShiftsById(id);
			return View(shift);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ShiftEdit(int id, Shift shift)
		{
			await _employeeRepository.UpdateShift(shift);
			return RedirectToAction(nameof(Shifts));
		}

		public IActionResult ShiftCreate()
		{
			return View(new Shift());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ShiftCreate(Shift shift)
		{
			if (ModelState.IsValid)
			{
				await _employeeRepository.CreateShift(shift);
				return RedirectToAction(nameof(Shifts));
			}
			return View(shift);
		}

		public async Task<IActionResult> ShiftDelete(int? id)
		{
			var shift = await _employeeRepository.GetShiftsById(id);
			return View(shift);
		}

		[HttpPost, ActionName("ShiftDelete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ShiftDeleteConfirmed(int id)
		{
			await _employeeRepository.DeleteShift(id);
			return RedirectToAction(nameof(Shifts));
		}
	}
}
