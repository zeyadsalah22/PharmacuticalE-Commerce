using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmacuticalE_Commerce.Models
{
    [MetadataType(typeof(ShippingAddressMetadata))]
    public partial class ShippingAddressDataAnnotation
    {
    }
    public class ShippingAddressMetadata
    {
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City cannot be longer than 100 characters.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "ZIP code is required.")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid ZIP code format.")]
        public string ZIP { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; } = null!;

        [Required]
        public string UserId { get; set; }

        [Display(Name = "Default Address")]
        public bool? IsDefault { get; set; }

        [Display(Name = "Deleted")]
        public bool? IsDeleted { get; set; }
    }
}
