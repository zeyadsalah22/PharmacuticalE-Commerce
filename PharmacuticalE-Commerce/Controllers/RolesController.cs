using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Controllers
{
    [Authorize(Roles = "Admin,HR")]
	public class RolesController : Controller
	{
		private readonly IRoleRepository _roleRepository;

		public RolesController(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
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
			await _roleRepository.Delete(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
