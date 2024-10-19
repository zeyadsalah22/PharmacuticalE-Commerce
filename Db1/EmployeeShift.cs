using System;
using System.Collections.Generic;

namespace Db1;

public partial class EmployeeShift
{
    public int EmployeeId { get; set; }

    public int ShiftId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Shift Shift { get; set; } = null!;
}
