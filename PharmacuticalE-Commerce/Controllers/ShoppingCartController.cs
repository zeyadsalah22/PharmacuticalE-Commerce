using Microsoft.AspNetCore.Mvc;
using PharmacuticalE_Commerce.Repositories.Implements;
using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartRepository _shoppingRepo;
        private readonly UserRepository _userRepo;
        private readonly CartItemRepository _cartItemRepo;
        private readonly ProductRepository _prodRepo;
        private readonly CartRepository _cartRepo;


        public ShoppingCartController(ShoppingCartRepository shoppingRepo,
                                      UserRepository userRepo,
                                      CartItemRepository cartItemRepo,
                                      ProductRepository prodRepo,
                                      CartRepository cartRepo)
        {
            _shoppingRepo = shoppingRepo;
            _userRepo = userRepo;
            _cartItemRepo = cartItemRepo;
            _prodRepo = prodRepo;
            _cartRepo = cartRepo;
        }

        public IActionResult Index(int? id)
        {
            var userActiveCart = _shoppingRepo.GetActiveCartOfUser(id);
            return View(userActiveCart);
        }

        public IActionResult AddToCart(int userId, int productId, int quantity)
        {
            if(_userRepo.GetById(userId) is null ||
               _prodRepo.GetById(productId) is null)
            {
                return Content("No user or No product!");
            }

            var userActiveCart = _shoppingRepo.GetActiveCartOfUser(userId);
            var cartToAdd = new Cart
            {
                Type = "Shopping",
                UserId = userId
            };

            if (userActiveCart is null) _cartRepo.Create(cartToAdd);
            
            _cartItemRepo.Create(new CartItem
            {
                CartId = cartToAdd.CartId,
                ProductId = productId,
                Quantity = quantity,
                IsSelected = true
            });

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int userId, int productId)
        {
            if (_userRepo.GetById(userId) is null ||
               _prodRepo.GetById(productId) is null)
            {
                return Content("No user or No product or no Active cart or No cart item!");
            }

            var userActiveCart = _shoppingRepo.GetActiveCartOfUser(userId);
            if(userActiveCart is null)
                return Content("No user or No product or no Active cart or No cart item!");

            var cartItem = _cartItemRepo.GetById(userActiveCart.Cart.UserId, productId);
            if(cartItem is null)
                return Content("No user or No product or no Active cart or No cart item!");

            cartItem.IsSelected = false;
            _cartItemRepo.Update(cartItem);

            return RedirectToAction("Index");
        }

        public IActionResult UpdateCart(int userId, int productId, int quantity)
        {
            if (_userRepo.GetById(userId) is null ||
               _prodRepo.GetById(productId) is null)
            {
                return Content("No user or No product or no Active cart or No cart item!");
            }

            var userActiveCart = _shoppingRepo.GetActiveCartOfUser(userId);
            if (userActiveCart is null)
                return Content("No user or No product or no Active cart or No cart item!");

            var cartItem = _cartItemRepo.GetById(userActiveCart.Cart.UserId, productId);
            if (cartItem is null)
                return Content("No user or No product or no Active cart or No cart item!");

            cartItem.Quantity = quantity;
            _cartItemRepo.Update(cartItem);

            return RedirectToAction("Index");
        }
    }
}
