using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacuticalE_Commerce.Models
{
    [ModelMetadataType(typeof(CartMetaData))]
    public partial class ShoppingCart
    {
    }

    public class ShoppingCartMetaData
    {
        [Key]
        [ForeignKey("Cart")]
        public int ShoppingCartId { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public virtual Cart Cart { get; set; } = null!;
    }
}
