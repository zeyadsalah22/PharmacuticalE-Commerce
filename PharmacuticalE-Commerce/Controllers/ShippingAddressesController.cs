﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using System.Security.Claims;

namespace PharmacuticalE_Commerce.Controllers
{
	public class ShippingAddressesController : Controller
	{
		private readonly IShippingAddressRepository _repository;

		public ShippingAddressesController(IShippingAddressRepository repository)
		{
			_repository = repository;
		}

		public async Task<IActionResult> Index()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var pharmacySystemContext = await _repository.GetShippingAddressByUserId(userId);
			return View(pharmacySystemContext);
		}


		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var shippingAddress = await _repository.GetById(id);

			if (shippingAddress == null || User.FindFirstValue(ClaimTypes.NameIdentifier) != shippingAddress.UserId)
			{
				return NotFound();
			}
			return View(shippingAddress);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("AddressId,UserId,Address,City,ZIP,Phone,IsDefault,IsDeleted")] ShippingAddress shippingAddress)
		{
			if (id != shippingAddress.AddressId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _repository.Update(shippingAddress);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await ShippingAddressExists(shippingAddress.AddressId))
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
			return View(shippingAddress);
		}


		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var shippingAddress = await _repository.GetById(id);
			if (shippingAddress == null || User.FindFirstValue(ClaimTypes.NameIdentifier) != shippingAddress.UserId)
			{
				return NotFound();
			}
			await _repository.Delete(id);
			return RedirectToAction(nameof(Index));
		}

		private async Task<bool> ShippingAddressExists(int id)
		{
			var addresses = await _repository.GetAll();
			return addresses.Any(e => e.AddressId == id);
		}
	}
}
