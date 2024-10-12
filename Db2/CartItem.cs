using System;
using System.Collections.Generic;

namespace Db2;

public partial class CartItem
{
    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string ProductName { get; set; } = null!;

    public bool? IsSelected { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
