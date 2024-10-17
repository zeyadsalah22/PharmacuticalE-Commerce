using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PharmacuticalE_Commerce.Viewmodels
{
    [Keyless]
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
