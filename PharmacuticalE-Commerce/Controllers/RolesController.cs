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

        // GET: Roles
        public IActionResult Index()
        {
            return View(_roleRepository.GetAll());
        }

        // GET: Roles/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RoleId,Title,Description")] Role role)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.Create(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RoleId,Title,Description")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _roleRepository.Update(role);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_roleRepository.GetById(role.RoleId) == null)
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
            return View(role);
        }

        // GET: Roles/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _roleRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
