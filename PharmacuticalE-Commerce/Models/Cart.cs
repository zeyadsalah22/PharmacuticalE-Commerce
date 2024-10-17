using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public string Type { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public virtual Prescription Prescription { get; set; } = null!;

    public virtual ShoppingCart ShoppingCart { get; set; } = null!;
    
    public virtual User User { get; set; } = null!;
}
