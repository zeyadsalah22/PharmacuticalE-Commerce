using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using System.Security.Claims;

namespace PharmacuticalE_Commerce.Controllers
{
	[Authorize]
	public class CartsController : Controller
	{
		private readonly ICartRepository _cartRepository;
		private readonly IProductRepository _productRepository;

		public CartsController(ICartRepository cartRepository, IProductRepository productRepository)
		{
			_cartRepository = cartRepository;
			_productRepository = productRepository;
		}

		public async Task<IActionResult> Index()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var activeCart = await _cartRepository.GetActiveCartByUserAsync(userId);

			if (activeCart == null)
			{
				var cart = new Cart { UserId = userId, CartItems = new List<CartItem>(), Status = true };
				await _cartRepository.AddCartAsync(cart);
				return View(cart);
			}

			return View(activeCart);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddCartItem(int productId, int quantity)
		{
			if (quantity < 1)
			{
				TempData["ErrorMessage"] = "Quantity must be more than 1";
				return RedirectToAction("CardDetails", "Products", new { Id = productId });
			}
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var activeCart = await _cartRepository.GetActiveCartByUserAsync(userId);

			if (activeCart == null)
			{
				var cart = new Cart { UserId = userId, CartItems = new List<CartItem>(), Status = true };
				await _cartRepository.AddCartAsync(cart);
				activeCart = cart;
			}

			var product = await _productRepository.GetById(productId);
			if (product == null)
			{
				return NotFound("Product not found.");
			}
			if (quantity > product.Stock)
			{
				TempData["ErrorMessage"] = "Not enough stock.";
				return RedirectToAction("CardDetails", "Products", new { Id = productId });

			}

			var existingCartItem = activeCart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
			if (existingCartItem != null)
			{
				existingCartItem.Quantity += quantity;
				await _cartRepository.UpdateCartAsync(activeCart);
			}
			else
			{
				var cartItem = new CartItem
				{
					CartId = activeCart.CartId,
					ProductId = productId,
					Quantity = quantity
				};

				await _cartRepository.AddCartItemAsync(cartItem);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RemoveCartItem(int cartId, int productId)
		{
			var cart = await _cartRepository.GetCartByIdAsync(cartId);
			if (cart == null || cart.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
			{
				return Unauthorized();
			}

			var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
			if (cartItem == null)
			{
				return NotFound("Item not found in the cart.");
			}

			await _cartRepository.RemoveCartItemAsync(cartItem);

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> GetPartialCart()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var activeCart = await _cartRepository.GetActiveCartByUserAsync(userId);

			if (activeCart == null)
			{
				var cart = new Cart { UserId = userId, CartItems = new List<CartItem>(), Status = true };
				await _cartRepository.AddCartAsync(cart);
				return View(cart);
			}

			return View(activeCart);
		}
	}
}
