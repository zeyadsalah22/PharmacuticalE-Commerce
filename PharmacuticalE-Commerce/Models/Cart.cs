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

    public decimal TotalPrice
    {
        get
        {
            DateTime currentDate = DateTime.Now;
            decimal totalPrice = 0;

            foreach (var item in CartItems)
            {
                decimal itemPrice = item.Product.Price;
                decimal discountValue = item.Product.Discount?.ValuePct ?? 0;
                decimal quantity = item.Quantity;

                if (item.Product.Discount != null && currentDate >= item.Product.Discount.StartDate && currentDate <= item.Product.Discount.EndDate)
                {
                    itemPrice = item.Product.Price * (1 - (discountValue / 100));
                }

                totalPrice += itemPrice * quantity;
            }

            return totalPrice;
        }
    }
}
