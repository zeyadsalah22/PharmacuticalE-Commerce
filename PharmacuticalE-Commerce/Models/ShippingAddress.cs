using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class ShippingAddress
{
    public int AddressId { get; set; }

    public string UserId { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string ZIP { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool? IsDefault { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
