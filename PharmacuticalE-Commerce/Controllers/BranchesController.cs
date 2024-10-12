using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Controllers
{
    public class BranchesController : Controller
    {
        private readonly IBranchRepository _branchRepository;

        public BranchesController(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public IActionResult Index()
        {
            var branches = _branchRepository.GetAllBranches();
            return View(branches);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BranchId,Address")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                _branchRepository.AddBranch(branch);
                _branchRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = _branchRepository.GetBranchById(id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BranchId,Address")] Branch branch)
        {
            if (id != branch.BranchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _branchRepository.UpdateBranch(branch);
                    _branchRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_branchRepository.BranchExists(branch.BranchId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = _branchRepository.GetBranchById(id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _branchRepository.DeleteBranch(id);
            _branchRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
