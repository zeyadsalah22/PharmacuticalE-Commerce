using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int CartId { get; set; }

    public int UserId { get; set; }

    public int AddressId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal? ShippingPrice { get; set; }

    public string? PromoCode { get; set; }

    public virtual ShippingAddress Address { get; set; } = null!;

    public virtual Cart Cart { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
