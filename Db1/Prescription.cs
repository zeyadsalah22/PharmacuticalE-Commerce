using System;
using System.Collections.Generic;

namespace Db1;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public string Photo { get; set; } = null!;

    public int CartId { get; set; }

    public string Status { get; set; } = null!;

    public int EmployeeId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
