using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace PharmacuticalE_Commerce.Models
{
    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order
    {
    }

    public class OrderMetadata
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey("Cart")]
        [Display(Name = "Cart")]
        public int CartId { get; set; }

        [Required]
        [ForeignKey("User")]
        [Display(Name = "User")]
        public string UserId { get; set; }

        [Required]
        [ForeignKey("ShippingAddress")]
        [Display(Name = "Shipping Address")]
        public int ShippingAddressId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Order Date")]
        public DateTime? OrderDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Shipping price cannot be negative.")]
        [Display(Name = "Shipping Price")]
        public decimal? ShippingPrice { get; set; }

        [StringLength(50, ErrorMessage = "Promo code cannot exceed 50 characters.")]
        [Display(Name = "Promo Code")]
        public string? PromoCode { get; set; }

        // Navigation properties

        [Required]
        [Display(Name = "Shipping Address")]
        public virtual ShippingAddress Address { get; set; } = null!;

        [Required]
        [Display(Name = "Cart")]
        public virtual Cart Cart { get; set; } = null!;

        [Required]
        [Display(Name = "User")]
        public virtual User User { get; set; } = null!;
    }
}
