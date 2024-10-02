using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
