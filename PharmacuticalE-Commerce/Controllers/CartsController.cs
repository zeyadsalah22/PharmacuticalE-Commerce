using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PharmacuticalE_Commerce.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;  // To fetch product details

        public CartsController(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        // Index: Show active cart for the current user
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var activeCart = await _cartRepository.GetActiveCartByUserAsync(userId);

            if (activeCart == null)
            {
                var cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                await _cartRepository.AddCartAsync(cart);
                return View(cart); // Return an empty cart if no active cart exists
            }

            return View(activeCart);
        }

        // Adding a new item to the cart or updating quantity if it already exists
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCartItem(int cartId, int productId, int quantity)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            if (cart == null || cart.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();  // Ensure the user owns the cart
            }

            var product = _productRepository.GetById(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Check if the cart already contains the item
            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingCartItem != null)
            {
                // Update quantity if item already exists
                existingCartItem.Quantity += quantity;
                await _cartRepository.UpdateCartAsync(cart);
            }
            else
            {
                // Add a new cart item
                var cartItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = productId,
                    Quantity = quantity
                };

                await _cartRepository.AddCartItemAsync(cartItem);
            }

            return RedirectToAction(nameof(Index));
        }

        // Remove an item from the cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCartItem(int cartId, int productId)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            if (cart == null || cart.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();  // Ensure the user owns the cart
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
            {
                return NotFound("Item not found in the cart.");
            }

            // Remove the cart item
            await _cartRepository.RemoveCartItemAsync(cartItem);

            return RedirectToAction(nameof(Index));
        }
    }
}
