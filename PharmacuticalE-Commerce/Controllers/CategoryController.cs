﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
namespace Categories.Controllers
{

	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _repository;
		private readonly IProductRepository _productRepository;

		public CategoryController(ICategoryRepository categoryRepository, IProductRepository productRepository)
		{
			_repository = categoryRepository;
			_productRepository = productRepository;
		}

		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Index()
		{
			var categories = await _repository.GetAll();
			return View(categories);
		}

		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Details(int id)
		{
			var category = await _repository.GetByIdWithParent(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Create()
		{
			ViewData["CategoryId"] = new SelectList(await _repository.GetParents(), "CategoryId", "Name");
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Create(Category category)
		{
			if (category.ParentCategoryId == 0)
			{
				category.ParentCategoryId = null;
			}

			var existingCategory = await _repository.GetCategoryByName(category.Name);
			if (existingCategory != null)
			{
				TempData["Error"] = "Category already exists.";
				return View(category);
			}

			if (ModelState.IsValid)
			{
				await _repository.Create(category);
				return RedirectToAction(nameof(Index));
			}

			return View(category);
		}


		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Edit(int id)
		{
			var category = await _repository.GetByIdWithParent(id);
			if (category == null)
			{
				return NotFound();
			}
			ViewData["CategoryId"] = new SelectList(await _repository.GetParents(), "CategoryId", "Name", category.ParentCategoryId);

			return View(category);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Edit(Category category)
		{
			if (category.ParentCategoryId == 0)
			{
				category.ParentCategoryId = null;
			}
			if (ModelState.IsValid)
			{
				await _repository.Update(category);
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Delete(int id)
		{
			var category = await _repository.GetByIdWithParent(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}


		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var category = await _repository.GetByIdWithParent(id);
			if (category != null)
			{
				if (!category.ParentCategoryId.HasValue)
				{
					if ((await _repository.GetChildsByparent(category.CategoryId)).Count() > 0)
					{
						TempData["Error"] = "Delete the category childs first";
						return RedirectToAction("Delete", new { id });
					}
				}
				else
				{
					if ((await _productRepository.GetProductsByCategory(id)).Count() > 0)
					{
						TempData["Error"] = "Delete the category products first";
						return RedirectToAction("Delete", new { id });
					}
				}
				await _repository.Delete(id);
			}
			return RedirectToAction(nameof(Index));

		}
	}
}
