using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories;
using PharmacuticalE_Commerce.Repositories.Implements;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PharmacuticalE_Commerce.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IShippingAddressRepository _shippingAddressRepository;
        private readonly decimal ShippingPrice = 50.00M; 
        public OrdersController(IOrderRepository orderRepository, ICartRepository cartRepository, IShippingAddressRepository shippingAddressRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _shippingAddressRepository = shippingAddressRepository;
        }

        // GET: Orders/Index
        public IActionResult Index()
        {
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        public IActionResult ListOrders()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _orderRepository.GetOrdersByUserId(userId);
            return View(orders);
        }

        public IActionResult Dashboard()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _orderRepository.GetOrdersByUserId(userId);
            return View(orders);
        }

        // GET: Orders/Details/5
        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetByIdWithDetails(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult OrderView(int id)
        {
            var order = _orderRepository.GetByIdWithDetails(id);
            if (order == null || order.UserId!= User.FindFirstValue(ClaimTypes.NameIdentifier))
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

        // GET: Orders/Create
        public IActionResult Create()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ShippingAddresses"] = new SelectList(_shippingAddressRepository.GetShippingAddressByUserId(userId), "AddressId", "Address");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Create(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public IActionResult Edit(int id)
        {
            var order = _orderRepository.GetByIdWithDetails(id);
            if (order == null)
            {
                return NotFound();
            }
            string userId = order.UserId;
            ViewData["ShippingAddresses"] = new SelectList(_shippingAddressRepository.GetShippingAddressByUserId(userId), "AddressId", "Address");
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, int ShippingAddressId, string Status)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = Status;
            order.ShippingAddressId = ShippingAddressId;
            if (ModelState.IsValid)
            {
                _orderRepository.Update(order);
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            var order = _orderRepository.GetByIdWithDetails(id);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = "Canceled";
            if (ModelState.IsValid)
            {
                _orderRepository.Update(order);
                foreach(var item in order.Cart.CartItems)
                {
                    item.Product.Stock += item.Quantity;
                }
            }

            return RedirectToAction(nameof(ListOrders));

        }

        // GET: Orders/Delete/5
        public IActionResult Delete(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order != null)
            {
                _orderRepository.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PlaceOrder()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ShippingAddresses"] = new SelectList(_shippingAddressRepository.GetShippingAddressByUserId(userId), "AddressId", "Address");
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
                return RedirectToAction("Index", "Carts");  // Redirect to the cart if it's empty
            }
            foreach(var item in cart.CartItems) {
                if (item.Product.Stock < item.Quantity)
                {
                    ViewBag.ErrorMessage = $"{item.Product.Name} in your cart is out of stock";
                    return RedirectToAction("Index", "Carts");  
                }
            }
            if (shippingAddress.AddressId != 0)
            {
                shippingAddress = _shippingAddressRepository.GetById(shippingAddress.AddressId);
                if (shippingAddress == null || shippingAddress.UserId != userId)
                {
                    return NotFound();
                }
            }
            else {
                ModelState.Remove("AddressId");
                ModelState.Remove("UserId");
                ModelState.Remove("User");
                if (!ModelState.IsValid)
				{
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
                _shippingAddressRepository.Create(shippingAddress);
            }
                
            // Calculate the total price of the cart
            decimal totalAmount = cart.CartItems.Sum(item => item.Quantity * item.Product.Price);

            // Create a new order entity
            var order = new Order
            {
                UserId = userId,
                ShippingAddress = shippingAddress,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount+ ShippingPrice,
                ShippingPrice = ShippingPrice,
                Cart = cart,
                Status = "Pending"
            };

            // Save the order to the database
            _orderRepository.Create(order);

            // Clear the cart after placing the order
            foreach(var item in cart.CartItems) {
                item.Product.Stock -= item.Quantity;
            }
            cart.Status = false;
            await _cartRepository.UpdateCartAsync(cart);

            return RedirectToAction(nameof(ListOrders));
        }

    }
}
