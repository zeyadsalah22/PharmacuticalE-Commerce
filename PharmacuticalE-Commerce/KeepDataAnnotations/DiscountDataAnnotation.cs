using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace PharmacuticalE_Commerce.Models
{
	[ModelMetadataType(typeof(DiscountMetaData))]
	public partial class Discount
	{
	}

	public class DiscountMetaData
	{
		[Key]
		public int DiscountId { get; set; }

		[Range(0, 100, ErrorMessage = "Discount value must be between 0 and 100.")]
		public decimal? ValuePct { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		[FutureDate(ErrorMessage = "The end date cannot be in the past.")]
		public DateTime EndDate { get; set; }



		public virtual Product Product { get; set; } = null!;
	}
}
