using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class ShoppingCart
{
    public int ShoppingCartId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public virtual Cart Cart { get; set; } = null!;
}
