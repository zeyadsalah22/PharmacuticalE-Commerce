using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
namespace Categories.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;
        private readonly IProductRepository _productRepository;

        public CategoryController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _repository = categoryRepository;
            _productRepository = productRepository;
        }

     
        public IActionResult Index()
        {
            var categories = _repository.GetAll();
            return View(categories);
        }

       
        public IActionResult Details(int id)
        {
            var category = _repository.GetByIdWithParent(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

       
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_repository.GetParents(), "CategoryId", "Name");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.ParentCategoryId == 0)
            {
                category.ParentCategoryId = null;
            }
            if (ModelState.IsValid)
            {
                _repository.Create(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _repository.GetByIdWithParent(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_repository.GetParents(), "CategoryId", "Name", category.ParentCategoryId);

            return View(category);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.ParentCategoryId == 0)
            {
                category.ParentCategoryId = null;
            }
            if (ModelState.IsValid)
            {
                _repository.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _repository.GetByIdWithParent(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _repository.GetByIdWithParent(id);
            if (category != null)
            {
                if (!category.ParentCategoryId.HasValue)
                {
                    if (_repository.GetChildsByparent(category.CategoryId).Count() > 0)
                    {
                        ModelState.AddModelError("", "Delete the category childs first");
                        return View("Delete", category);
                    }
                    _repository.Delete(id);
                }
                else
                {
                    if (_productRepository.GetProductsByCategory(id).Count()> 0)
                    {
                        ModelState.AddModelError("", "Delete the category products first");
                        return View("Delete", category);
                    }
                    _repository.Delete(id);
                }
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
