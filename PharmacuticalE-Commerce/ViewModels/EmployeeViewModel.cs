using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Shift> Shifts { get; set; }
    }
}
