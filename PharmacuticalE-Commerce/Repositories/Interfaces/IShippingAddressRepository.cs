using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IShippingAddressRepository : IRepository<ShippingAddress>
    {
        public IEnumerable<ShippingAddress> GetShippingAddressByUserId(string userId);
    }
}
