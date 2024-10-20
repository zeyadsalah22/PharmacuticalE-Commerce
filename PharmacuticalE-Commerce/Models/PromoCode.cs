using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class PromoCode
{
    public int PromoCodeId { get; set; }
    public string PromoCode1 { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal? Value { get; set; }

    public int UsageLimit { get; set; }

    public decimal? MinOrderAmount { get; set; }


}
