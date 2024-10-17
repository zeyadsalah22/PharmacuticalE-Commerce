using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            return View(_roleRepository.GetAll());
        }

        public IActionResult Details(int? id)
        {
            var role = _roleRepository.GetById(id);

            return View(role);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.Create(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        public IActionResult Edit(int? id)
        {

            var role = _roleRepository.GetById(id);

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Role role)
        {

            if (ModelState.IsValid)
            {
                
                _roleRepository.Update(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        public IActionResult Delete(int? id)
        {

            var role = _roleRepository.GetById(id);

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _roleRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
