using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class Attendance
{
    public int RecordId { get; set; }

    public int EmployeeId { get; set; }

    public int ShiftId { get; set; }

    public int BranchId { get; set; }

    public DateTime AttendedAt { get; set; }

    public DateTime? LeftAt { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Shift Shift { get; set; } = null!;
}
