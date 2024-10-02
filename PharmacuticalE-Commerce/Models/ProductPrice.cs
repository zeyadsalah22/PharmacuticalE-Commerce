using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class ProductPrice
{
    public int PriceId { get; set; }

    public int ProductId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal Price { get; set; }

    public virtual Product Product { get; set; } = null!;
}
