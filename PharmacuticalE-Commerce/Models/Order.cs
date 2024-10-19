using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public enum OrderStatus
{
    Pending,
    Delevering,
    Completed,
    Cancelled
}
public partial class Order
{
    public int OrderId { get; set; }

    public int CartId { get; set; }

    public string UserId { get; set; }

    public int ShippingAddressId { get; set; }

    public DateTime? OrderDate { get; set; } = DateTime.Now;

    public decimal TotalAmount { get; set; }

	public int TotalItems
    {
        get
        {
            if (Cart?.CartItems == null)
            {
                return 0;
            }
            return Cart.CartItems.Sum(item => item.Quantity);
        }
    }
	public decimal? ShippingPrice { get; set; }

    public bool IsPaid { get; set; }

    public string? PromoCode { get; set; }
    public OrderStatus? Status { get; set; }

    public virtual ShippingAddress ShippingAddress { get; set; } = null!;

    public virtual Cart Cart { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
