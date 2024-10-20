using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories;
using PharmacuticalE_Commerce.Repositories.Implements;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using Stripe.Checkout;
using Stripe;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PharmacuticalE_Commerce.Controllers
{
    [Authorize]
	public class OrdersController : Controller
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ICartRepository _cartRepository;
		private readonly IShippingAddressRepository _shippingAddressRepository;
		private readonly ICartItemRepository _cartitemRepository;
		private readonly IConfiguration _configuration;
		private readonly decimal ShippingPrice = 50.00M;
		public OrdersController(IOrderRepository orderRepository, ICartRepository cartRepository, IShippingAddressRepository shippingAddressRepository,	ICartItemRepository cartItemRepository, IConfiguration configuration)
		{
			_orderRepository = orderRepository;
			_cartRepository = cartRepository;
			_shippingAddressRepository = shippingAddressRepository;
			_configuration = configuration;
			_cartitemRepository = cartItemRepository;
		}

		// GET: Orders/Index
		public async Task<IActionResult> Index()
		{
			var orders = await _orderRepository.GetAll();
			return View(orders);
		}

		public async Task<IActionResult> ListOrders()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var orders = await _orderRepository.GetOrdersByUserId(userId);
			return View(orders);
		}

		public async Task<IActionResult> Dashboard()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var orders = await _orderRepository.GetOrdersByUserId(userId);
			return View(orders);
		}

		// GET: Orders/Details/5
		public async Task<IActionResult> Details(int id)
		{
			var order = await _orderRepository.GetByIdWithDetails(id);
			if (order == null)
			{
				return NotFound();
			}

			return View(order);
		}

		public async Task<IActionResult> OrderView(int id)
		{
			var order = await _orderRepository.GetByIdWithDetails(id);
			if (order == null || order.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
			{
				return NotFound();
			}

			return View(order);
		}

		public async Task<IActionResult> OrderPartial()
		{
			var cart = await _cartRepository.GetActiveCartByUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
			if (cart == null)
			{
				return NotFound();
			}
			ViewData["ShippingPrice"] = ShippingPrice;
			return View(cart);
		}

		// GET: Orders/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			var order = await _orderRepository.GetByIdWithDetails(id);
			if (order == null)
			{
				return NotFound();
			}
			string userId = order.UserId;
			ViewData["ShippingAddresses"] = new SelectList(await _shippingAddressRepository.GetShippingAddressByUserId(userId), "AddressId", "Address");
			return View(order);
		}

		// POST: Orders/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, int ShippingAddressId, OrderStatus Status)
		{
			var order = await _orderRepository.GetById(id);
			if (order == null)
			{
				return NotFound();
			}
			order.Status = Status;
			order.ShippingAddressId = ShippingAddressId;
			if (ModelState.IsValid)
			{
				await _orderRepository.Update(order);
				return RedirectToAction(nameof(Index));
			}

			return View(order);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Cancel(int id)
		{
			var order = await _orderRepository.GetByIdWithDetails(id);
			if (order == null)
			{
				return NotFound();
			}
			order.Status = OrderStatus.Cancelled;
			if (ModelState.IsValid)
			{
				await _orderRepository.Update(order);
				foreach (var item in order.Cart.CartItems)
				{
					item.Product.Stock += item.Quantity;
				}
			}

			return RedirectToAction(nameof(ListOrders));

		}

		// GET: Orders/Delete/5
		public async Task<IActionResult> Delete(int id)
		{
			var order = await _orderRepository.GetById(id);
			if (order == null)
			{
				return NotFound();
			}

			return View(order);
		}

		// POST: Orders/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var order = await _orderRepository.GetById(id);
			if (order != null)
			{
				await _orderRepository.Delete(id);
			}
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> PlaceOrder()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			ViewData["ShippingAddresses"] = new SelectList(await _shippingAddressRepository.GetShippingAddressByUserId(userId), "AddressId", "Address");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> PlaceOrder(ShippingAddress shippingAddress)
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			// Get the active cart for the user
			var cart = await _cartRepository.GetActiveCartByUserAsync(userId);
			if (cart == null || cart.CartItems.Count == 0)
			{
				ModelState.AddModelError(string.Empty, "Your cart is empty");
				return View(shippingAddress);  
			}
			foreach (var item in cart.CartItems)
			{
				if (item.Product.Stock < item.Quantity)
				{
					ModelState.AddModelError(string.Empty, $"{item.Product.Name} in your cart is out of stock");
					return View(shippingAddress);
				}
			}
			if (shippingAddress.AddressId != 0)
			{
				shippingAddress = await _shippingAddressRepository.GetById(shippingAddress.AddressId);
				if (shippingAddress == null || shippingAddress.UserId != userId)
				{
					return NotFound();
				}
			}
			else
			{
				ModelState.Remove("AddressId");
				ModelState.Remove("UserId");
				ModelState.Remove("User");
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError(string.Empty, "Please select or enter a shipping address");
					return View(shippingAddress);
				}
				shippingAddress = new ShippingAddress
				{
					Address = shippingAddress.Address,
					UserId = userId,
					ZIP = shippingAddress.ZIP,
					City = shippingAddress.City,
					Phone = shippingAddress.Phone
				};
				await _shippingAddressRepository.Create(shippingAddress);
			}

			foreach (CartItem item in cart.CartItems)
			{
				if(item.Product.Discount!=null && item.Product.Discount.StartDate <= DateTime.Now && item.Product.Discount.EndDate >= DateTime.Now)
				{
					item.FinalPrice = (1 - ((item.Product.Discount?.ValuePct ?? 0) / 100)) * item.Product.Price;
                }
				else
				{
					item.FinalPrice = item.Product.Price;
				}
				_cartitemRepository.Update(item);
			}
			// Calculate the total price of the cart
			decimal totalAmount = cart.CartItems.Sum(item => item.Quantity * (item.FinalPrice ?? item.Product.Price));

			var order = new Order
			{
				UserId = userId,
				ShippingAddress = shippingAddress,
				OrderDate = DateTime.Now,
				TotalAmount = totalAmount + ShippingPrice,
				ShippingPrice = ShippingPrice,
				Cart = cart,
				Status = OrderStatus.Pending
			};

			await _orderRepository.Create(order);

			// Clear the cart after placing the order
			foreach (var item in cart.CartItems)
			{
				item.Product.Stock -= item.Quantity;
			}
			cart.Status = false;
			await _cartRepository.UpdateCartAsync(cart);

			return RedirectToAction("OrderView", new { id = order.OrderId });
		}

	}
}
