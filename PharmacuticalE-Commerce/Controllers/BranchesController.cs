using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Implements;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Controllers
{
    [Authorize(Roles = "Admin")]
	public class BranchesController : Controller
	{
		private readonly IBranchRepository _branchRepository;
		private readonly IEmployeeRepository _employeeRepository;

		public BranchesController(IBranchRepository branchRepository , IEmployeeRepository employeeRepository)
		{
			_branchRepository = branchRepository;
			_employeeRepository = employeeRepository;
		}

		public async Task<IActionResult> Index()
		{
			var branches = await _branchRepository.GetAll();
			return View(branches);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Branch branch)
		{
			await _branchRepository.Create(branch);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int? id)
		{
			var branch = await _branchRepository.GetById(id);
			return View(branch);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Branch branch)
		{
			await _branchRepository.Update(branch);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id)
		{
			var branch = await _branchRepository.GetById(id);
			return View(branch);
		}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await _employeeRepository.GetEmployeesByBranchId(id);
            if (employees.Any())
            {
				TempData["Error"] = "Delete employees first";
                var branch = await _branchRepository.GetById(id);
                return View(branch);
            }

            await _branchRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
