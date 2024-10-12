using System;
using System.Collections.Generic;

namespace Db1;

public partial class Shift
{
    public int ShiftId { get; set; }

    public TimeOnly FromTime { get; set; }

    public TimeOnly ToTime { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
