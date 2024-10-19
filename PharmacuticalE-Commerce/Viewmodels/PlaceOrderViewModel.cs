using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Viewmodels
{
    public class PlaceOrderViewModel
    {
        public int AddressId { get; set; }

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string ZIP { get; set; } = null!;
        public virtual Cart Cart { get; set; } = null!;
    }
}
