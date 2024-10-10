using System;
using System.Collections.Generic;

namespace Db2;

public partial class ProductAttribute
{
    public int AttributeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Pav> Pavs { get; set; } = new List<Pav>();
}
