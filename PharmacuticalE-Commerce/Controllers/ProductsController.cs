using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day6Mydemo.Models;
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
        private readonly ICategoryRepository _categoryRepository;
        
        public ProductsController(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = _repository.GetAllWithCategories();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _repository.GetByIdWithCategories(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetChilds(), "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([ModelBinder(BinderType=typeof(ProductBinder))] Product product)
        {
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                _repository.Create(product);
                TempData["MessageAdd"] = $"Product {product.Name} Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetChilds(), "CategoryId", "Name",product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _repository.GetByIdWithCategories(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetChilds(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _repository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetChilds(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _repository.GetByIdWithCategories(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _repository.GetById(id);
            if (product != null)
            {
                _repository.Delete(id);
                TempData["MessageDelete"] = $"Product {product.Name} Deleted Successfully";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Gallery()
        {
            var products = _repository.GetAllWithCategories();
            return View(products);
        }
        private bool ProductExists(int id)
        {
            return _repository.GetAll().Any(e => e.ProductId == id);
        }
    }
}
