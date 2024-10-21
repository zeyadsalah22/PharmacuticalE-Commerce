using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Controllers
{
	[Authorize(Roles = "Admin,HR")]
	public class RolesController : Controller
	{
		private readonly IRoleRepository _roleRepository;
		private readonly IEmployeeRepository _employeeRepository;

		public RolesController(IRoleRepository roleRepository, IEmployeeRepository employeeRepository)
		{
			_roleRepository = roleRepository;
			_employeeRepository = employeeRepository;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _roleRepository.GetAll());
		}

		public async Task<IActionResult> Details(int? id)
		{
			var role = await _roleRepository.GetById(id);

			return View(role);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Role role)
		{
			if (ModelState.IsValid)
			{
				await _roleRepository.Create(role);
				return RedirectToAction(nameof(Index));
			}
			return View(role);
		}

		public async Task<IActionResult> Edit(int? id)
		{

			var role = await _roleRepository.GetById(id);

			return View(role);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Role role)
		{

			if (ModelState.IsValid)
			{

				await _roleRepository.Update(role);
				return RedirectToAction(nameof(Index));
			}
			return View(role);
		}

		public async Task<IActionResult> Delete(int? id)
		{

			var role = await _roleRepository.GetById(id);

			return View(role);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var employees = await _employeeRepository.GetEmployeesByRoleId(id);
			if (employees.Any())
			{
				TempData["Error"] = "Delete employees with this role first.";
				var role = await _roleRepository.GetById(id);
				return View(role);
			}

			await _roleRepository.Delete(id);
			return RedirectToAction(nameof(Index));
		}

	}
}
