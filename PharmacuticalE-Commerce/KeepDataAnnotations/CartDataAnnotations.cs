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

        public string Type { get; set; } = null!;

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ShoppingCart ShoppingCart { get; set; } = null!;

        public virtual Prescription Prescription { get; set; } = null!;
        
        public virtual User User { get; set; } = null!;
    }
}
