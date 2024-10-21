using Day6Mydemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;

namespace PharmacuticalE_Commerce.Controllers
{
	public class ProductsController : Controller
	{
		[TempData]
		public string MessageAdd { get; set; }
		[TempData]
		public string MessageDelete { get; set; }
		private readonly IProductRepository _repository;
		private readonly IDiscountRepository _discountRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly ICartItemRepository _cartItemRepository;

		public ProductsController(IProductRepository repository, ICategoryRepository categoryRepository, IDiscountRepository discountRepository, ICartItemRepository cartItemRepository)
		{
			_repository = repository;
			_categoryRepository = categoryRepository;
			_discountRepository = discountRepository;
			_cartItemRepository = cartItemRepository;
		}
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Index(string sortOrder, string categoryFilter, string searchString, int pageNumber = 1, int pageSize = 12)
		{
			ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
			ViewData["CurrentSort"] = sortOrder;
			var products = await _repository.GetAllWithCategories();
			if (!String.IsNullOrEmpty(categoryFilter))
			{
				ViewData["CategoryFilterParm"] = categoryFilter;
				products = products.Where(p => p.Category.Name.ToLower().Contains(categoryFilter.ToLower()) ||
				(p.Category.ParentCategory != null &&
				p.Category.ParentCategory.Name.ToLower().Contains(categoryFilter.ToLower())
				));
			}
			if (!String.IsNullOrEmpty(searchString))
			{
				ViewData["SearchStringParm"] = searchString;
				pageNumber = 1;
				products = products.Where(p => p.Name.ToLower().Contains(searchString.ToLower()));
			}
			switch (sortOrder)
			{
				case "name_desc":
					products = products.OrderByDescending(p => p.Name).ToList();
					break;
				case "price":
					products = products.OrderBy(p => p.Price).ToList();
					break;
				case "price_desc":
					products = products.OrderByDescending(p => p.Price).ToList();
					break;
				default:
					products = products.OrderBy(p => p.Name).ToList();
					break;
			}
			return View(await PaginatedList<Product>.CreateAsync(products, pageNumber, pageSize));
		}

		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _repository.GetByIdWithCategories(id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		public async Task<IActionResult> CardDetails(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _repository.GetByIdWithCategories(id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Create()
		{
			ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetChilds(), "CategoryId", "Name");
			return View();
		}

		[Authorize(Roles = "Admin,Moderator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([ModelBinder(BinderType = typeof(ProductBinder))] Product product)
		{
			ModelState.Remove("Category");
			if (ModelState.IsValid)
			{
				await _repository.Create(product);
				TempData["MessageAdd"] = $"Product {product.Name} Added Successfully";
				return RedirectToAction(nameof(Index));
			}
			ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetChilds(), "CategoryId", "Name", product.CategoryId);
			return View(product);
		}

		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _repository.GetByIdWithCategories(id);
			if (product == null)
			{
				return NotFound();
			}
			ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetChilds(), "CategoryId", "Name", product.CategoryId);
			return View(product);
		}

		[Authorize(Roles = "Admin,Moderator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [ModelBinder(BinderType = typeof(ProductBinder))] Product product)
		{
			if (id != product.ProductId)
			{
				return NotFound();
			}
			ModelState.Remove("Category");
			if (ModelState.IsValid)
			{
				try
				{
					await _repository.Update(product);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await ProductExists(product.ProductId))
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
			ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetChilds(), "CategoryId", "Name", product.CategoryId);
			return View(product);
		}

		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _repository.GetByIdWithCategories(id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		[Authorize(Roles = "Admin,Moderator")]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var product = await _repository.GetById(id);
			if (product != null)
			{
				// Check if the product is in any cart items
				var cartItems = await _cartItemRepository.GetCartItemsByProductId(id);
				if (cartItems.Any())
				{
					TempData["Error"] = $"Product {product.Name} is in a cart. Delete associated cart items first.";
					return RedirectToAction("Delete", new { id });
				}

				await _repository.Delete(id);
				TempData["MessageDelete"] = $"Product {product.Name} Deleted Successfully";
			}
			return RedirectToAction(nameof(Index));
		}


		public async Task<IActionResult> Gallery(string sortOrder, string categoryFilter, string searchString, int pageNumber = 1, int pageSize = 5)
		{
			ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
			ViewData["CurrentSort"] = sortOrder;
			var products = await _repository.GetAllWithCategories();
			if (!String.IsNullOrEmpty(categoryFilter))
			{
				ViewData["CategoryFilterParm"] = categoryFilter;
				products = products.Where(p => p.Category.Name.ToLower().Contains(categoryFilter.ToLower()) ||
				(p.Category.ParentCategory != null &&
				p.Category.ParentCategory.Name.ToLower().Contains(categoryFilter.ToLower())
				));
			}
			if (!String.IsNullOrEmpty(searchString))
			{
				ViewData["SearchStringParm"] = searchString;
				pageNumber = 1;
				products = products.Where(p => p.Name.ToLower().Contains(searchString.ToLower()));
			}
			switch (sortOrder)
			{
				case "name_desc":
					products = products.OrderByDescending(p => p.Name).ToList();
					break;
				case "price":
					products = products.OrderBy(p => p.Price).ToList();
					break;
				case "price_desc":
					products = products.OrderByDescending(p => p.Price).ToList();
					break;
				default:
					products = products.OrderBy(p => p.Name).ToList();
					break;
			}
			return View(await PaginatedList<Product>.CreateAsync(products, pageNumber, pageSize));
		}

		public async Task<IActionResult> GetRelatedProducts(string categoryFilter, int productId)
		{
			var products = await _repository.GetAllWithCategories();
			if (!String.IsNullOrEmpty(categoryFilter))
			{
				ViewData["CategoryFilterParm"] = categoryFilter;
				products = products.Where(p => p.ProductId != productId && (p.Category.Name.ToLower().Contains(categoryFilter.ToLower()) ||
				(p.Category.ParentCategory != null &&
				p.Category.ParentCategory.Name.ToLower().Contains(categoryFilter.ToLower())
				))).Take(4);
			}

			return View("_RelatedProductsPartial", products);
		}

		public async Task<IActionResult> GetOffers()
		{
			var products = await _repository.GetAll();
			var offers = products.Where(p => (p.Discount != null && p.Discount.EndDate >= DateTime.Today && p.Discount.StartDate <= DateTime.Today))
								.OrderByDescending(p => p.Discount.CreatedAt)
								.Take(8);
			return View("_OffersPartial", offers);
		}

		[HttpGet]
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> AddDiscount(int productId)
		{
			var product = await _repository.GetById(productId);
			if (product == null)
			{
				return NotFound("Product not found.");
			}

			ViewBag.ProductName = product.Name;
			ViewBag.ProductId = product.ProductId;
			return View(new Discount());
		}


		[HttpPost]
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> AddDiscount(int productId, Discount discount)
		{
			var product = await _repository.GetById(productId);
			if (product == null)
			{
				return NotFound("Product not found.");
			}
			ViewBag.ProductName = product.Name;
			ViewBag.ProductId = product.ProductId;

			ModelState.Remove("Product");
			discount.Product = product;
			if (!ModelState.IsValid)
			{
				return View(discount);
			}
			await _discountRepository.Create(discount);

			product.DiscountId = discount.DiscountId;
			await _repository.Update(product);

			TempData["MessageAdd"] = $"Discount added successfully to {product.Name}";
			return RedirectToAction(nameof(Details), new { id = productId });
		}

		[HttpGet]
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> ConfirmDeleteDiscount(int productId)
		{
			var product = await _repository.GetById(productId);
			if (product == null || product.DiscountId == null)
			{
				return NotFound("Product or discount not found.");
			}

			var discount = await _discountRepository.GetById(product.DiscountId.Value);
			if (discount == null)
			{
				return NotFound("Discount not found.");
			}

			ViewBag.ProductName = product.Name;
			ViewBag.ProductId = product.ProductId;
			return View(discount);
		}


		[HttpPost]
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> DeleteDiscount(int productId)
		{
			var product = await _repository.GetById(productId);
			if (product == null || product.DiscountId == null)
			{
				return NotFound("Product or discount not found.");
			}

			var discount = await _discountRepository.GetById(product.DiscountId.Value);
			if (discount != null)
			{
				await _discountRepository.Delete(discount.DiscountId);
			}

			product.DiscountId = null;
			await _repository.Update(product);

			TempData["MessageDelete"] = $"Discount removed successfully from {product.Name}";
			return RedirectToAction(nameof(Details), new { id = productId });
		}

		public async Task<IActionResult> GetCategoriesSideBar()
		{
			var categories = await _categoryRepository.GetParents();
			return View("_CategoriesSideBarPartial", categories);
		}

		public async Task<IActionResult> GetCategoriesNav()
		{
			var categories = await _categoryRepository.GetParents();
			return View("_CategoriesNavPartial", categories);
		}

		private async Task<bool> ProductExists(int id)
		{
			var products = await _repository.GetAll();
			return products.Any(e => e.ProductId == id);
		}
	}
}
