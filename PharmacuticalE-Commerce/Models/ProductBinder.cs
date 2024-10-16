using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PharmacuticalE_Commerce.Models;

namespace Day6Mydemo.Models
{
    public class ProductBinder : IModelBinder
    {
        private readonly long _maxFileSize = 5 * 1024 * 1024; // Maximum file size: 5 MB
        private readonly string _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");
        private string defaultPhoto = "default.jpeg";
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string? productIdValue = bindingContext.HttpContext.Request.Form["ProductId"];
            // var categoryId = int.Parse(bindingContext.HttpContext.Request.Form["CategoryId"]);
            var productName = bindingContext.HttpContext.Request.Form["Name"];
            var price = bindingContext.HttpContext.Request.Form["Price"];
            var serialNumber = bindingContext.HttpContext.Request.Form["SerialNumber"];
            var stock = bindingContext.HttpContext.Request.Form["Stock"];
            var Description = bindingContext.HttpContext.Request.Form["Description"];
            var oldPhoto = bindingContext.HttpContext.Request.Form["OldPhoto"]; // Hidden field for old photo
            var photo = bindingContext.HttpContext.Request.Form.Files["photo"];

            if (!decimal.TryParse(price, out _))
            {
                bindingContext.ModelState.AddModelError("Price", "Invalid Price");
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            if (!int.TryParse(stock, out _))
            {
                bindingContext.ModelState.AddModelError("Stock", "Invalid Stock");
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            if (!int.TryParse(bindingContext.HttpContext.Request.Form["CategoryId"], out int categoryId))
            {
                bindingContext.ModelState.AddModelError("CategoryId", "Category is required.");
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            string photoFilename = defaultPhoto;
            if (productIdValue !=null && oldPhoto != "")
            {
                photoFilename = oldPhoto;
            }
            if (photo != null && photo.Length > 0)
            {
                if (photo.Length > _maxFileSize)
                {
                    bindingContext.ModelState.AddModelError("Photo", "Photo exceeds the maximum size limit of 5MB.");
                    bindingContext.Result = ModelBindingResult.Failed();
                }
                string extension = Path.GetExtension(photo.FileName);
                photoFilename = productName + DateTime.Now.ToString("yyMMddhhssfff") + extension;
                var filepath = Path.Combine(_imageFolderPath, photoFilename);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
            }
            Product product = new Product
            {
                Name = productName,
                Price = decimal.Parse(price),
                Photo = photoFilename,
                Stock = int.Parse(stock),
                SerialNumber = serialNumber,
                CategoryId = categoryId,
                Description = Description
            };
            if (productIdValue != null)
            {
                product.ProductId = int.Parse(productIdValue);
            }

            bindingContext.Result = ModelBindingResult.Success(product);
        }
    }
}
