using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacuticalE_Commerce.Models
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
		[Display(Name = "Product Name")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "Please Enter Your Price")]
		[Range(0, 100000, ErrorMessage = "Price should be less than 100000")]
		public decimal Price { get; set; }

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


		// Add DiscountId as a foreign key
		[ForeignKey("Discount")]
		public int? DiscountId { get; set; }

		public virtual Discount? Discount { get; set; }
	}
}
