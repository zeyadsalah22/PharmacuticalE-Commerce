﻿using System;
using System.Collections.Generic;

namespace Db2;

public partial class Pav
{
    public int ProductId { get; set; }

    public int AttributeId { get; set; }

    public string Value { get; set; } = null!;

    public virtual ProductAttribute Attribute { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}