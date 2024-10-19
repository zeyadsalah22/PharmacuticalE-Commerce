using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Controllers
{
    [Authorize(Roles = "Admin")]
	public class BranchesController : Controller
	{
		private readonly IBranchRepository _branchRepository;

		public BranchesController(IBranchRepository branchRepository)
		{
			_branchRepository = branchRepository;
		}

		public async Task<IActionResult> Index()
		{
			var branches = await _branchRepository.GetAllBranches();
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
			await _branchRepository.AddBranch(branch);
			await _branchRepository.Save();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int? id)
		{
			var branch = await _branchRepository.GetBranchById(id);
			return View(branch);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Branch branch)
		{
			await _branchRepository.UpdateBranch(branch);
			await _branchRepository.Save();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id)
		{
			var branch = await _branchRepository.GetBranchById(id);
			return View(branch);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _branchRepository.DeleteBranch(id);
			await _branchRepository.Save();
			return RedirectToAction(nameof(Index));
		}
	}
}
