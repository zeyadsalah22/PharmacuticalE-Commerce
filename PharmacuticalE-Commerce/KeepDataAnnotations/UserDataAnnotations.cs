using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PharmacuticalE_Commerce.Models
{
    [ModelMetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }

    public class UserMetaData
    {
        [Key]
        public int UserId { get; set; }

        public string Fname { get; set; } = null!;

        public string Lname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();

    }
}
