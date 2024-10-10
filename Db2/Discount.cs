using System;
using System.Collections.Generic;

namespace Db2;

public partial class Discount
{
    public int DiscountId { get; set; }

    public decimal? ValuePct { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<PromoCode> PromoCodes { get; set; } = new List<PromoCode>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
