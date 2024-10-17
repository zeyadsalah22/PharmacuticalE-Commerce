using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PharmacuticalE_Commerce.Viewmodels
{
    [Keyless]
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
		public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
		[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
