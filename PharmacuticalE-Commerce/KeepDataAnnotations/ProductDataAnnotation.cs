using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.KeepDataAnnotations
{
    [ModelMetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }

    public class ProductMetaData
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
        public string Name { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Serial number is required")]
        [StringLength(50, ErrorMessage = "Serial number cannot exceed 50 characters")]
        public string SerialNumber { get; set; } = null!;

        public string? Photo { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        [Required]
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Pav> Pavs { get; set; } = new List<Pav>();

        public virtual ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();

        public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
