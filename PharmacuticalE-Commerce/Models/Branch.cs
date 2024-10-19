using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
