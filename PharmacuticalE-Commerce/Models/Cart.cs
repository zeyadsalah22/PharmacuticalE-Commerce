using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class Cart
{
    public int CartId { get; set; }

	public bool Status { get; set; }

	public DateTime? UpdatedAt { get; set; }

	public string UserId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public virtual User User { get; set; } = null!;
}
