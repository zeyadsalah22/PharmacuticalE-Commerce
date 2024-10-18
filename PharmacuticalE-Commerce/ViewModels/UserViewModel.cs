namespace PharmacuticalE_Commerce.ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> RoleNames { get; set; }
    }
}
