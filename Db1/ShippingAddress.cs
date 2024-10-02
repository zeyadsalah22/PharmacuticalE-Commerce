using System;
using System.Collections.Generic;

namespace Db1;

public partial class ShippingAddress
{
    public int AddressId { get; set; }

    public int UserId { get; set; }

    public string Address { get; set; } = null!;

    public bool? IsDefault { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
