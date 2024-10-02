using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class PromoCode
{
    public string PromoCode1 { get; set; } = null!;

    public int DiscountId { get; set; }

    public decimal? MinOrderAmount { get; set; }

    public decimal? MaxDiscountAmount { get; set; }

    public virtual Discount Discount { get; set; } = null!;
}
