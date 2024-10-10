using System;
using System.Collections.Generic;

namespace Db2;

public partial class ShoppingCart
{
    public int ShoppingCartId { get; set; }

    public int CartId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public virtual Cart Cart { get; set; } = null!;
}
