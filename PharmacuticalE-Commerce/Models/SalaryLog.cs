using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class SalaryLog
{
    public int RecordId { get; set; }

    public int EmployeeId { get; set; }

    public string ChangeType { get; set; } = null!;

    public decimal Value { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsPermanent { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
