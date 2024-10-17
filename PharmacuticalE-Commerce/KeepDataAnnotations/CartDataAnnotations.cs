using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacuticalE_Commerce.Models
{
    [ModelMetadataType(typeof(CartMetaData))]
    public partial class Cart
    {
    }

    public class CartMetaData
    {
        [Key]
        public int CartId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    
}
