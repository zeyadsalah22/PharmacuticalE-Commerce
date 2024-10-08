namespace PharmacuticalE_Commerce.ViewModels
{
    public class AttendancesViewModel
    {
        
            public int EmployeeId { get; set; }
            public int RecordId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string BranchAddress { get; set; }
            public int ShiftId { get; set; }
            public DateTime AttendedAt { get; set; }
            public DateTime? LeftAt { get; set; }
            public IEnumerable<int> ShiftIds { get; set; }
            public IEnumerable<string> Branch { get; set; }


    }
}
