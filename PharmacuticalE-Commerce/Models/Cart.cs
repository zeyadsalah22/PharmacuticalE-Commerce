using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public string Type { get; set; } = null!;

    public string UserId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    public virtual User User { get; set; } = null!;
}
