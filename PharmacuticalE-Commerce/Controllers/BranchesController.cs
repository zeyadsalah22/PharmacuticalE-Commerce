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
        public IActionResult Create(Branch branch)
        {

            _branchRepository.AddBranch(branch);
            _branchRepository.Save();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int? id)
        {

            var branch = _branchRepository.GetBranchById(id);

            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Branch branch)
        {

            _branchRepository.UpdateBranch(branch);
            _branchRepository.Save();

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int? id)
        {
 
            var branch = _branchRepository.GetBranchById(id);

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
