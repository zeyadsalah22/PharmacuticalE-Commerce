using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public int BranchId { get; set; }

    public decimal? Salary { get; set; }

    public DateTime? FirstDay { get; set; }

    public DateTime? CreatedAt { get; set; }

	public int ShiftId { get; set; }

	public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Branch Branch { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;


    public virtual Shift? Shift { get; set; }
}
