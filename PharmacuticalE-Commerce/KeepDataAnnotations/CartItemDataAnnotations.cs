using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacuticalE_Commerce.Models
{
    [ModelMetadataType(typeof(CartItemMetaData))]
    public partial class CartItem
    {
    }


    [PrimaryKey(nameof(CartId), nameof(ProductId))]
    public class CartItemMetaData
    {
        [ForeignKey("Cart")]
        public int CartId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public bool IsSelected { get; set; }

        public virtual Cart Cart { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
