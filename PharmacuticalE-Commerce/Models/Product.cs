using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string? Photo { get; set; }

    public int CategoryId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
