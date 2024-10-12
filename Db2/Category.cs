using System;
using System.Collections.Generic;

namespace Db2;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentCategoryId { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
