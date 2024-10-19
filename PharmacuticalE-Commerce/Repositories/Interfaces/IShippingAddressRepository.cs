using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface IShippingAddressRepository : IRepository<ShippingAddress>
	{
		Task<IEnumerable<ShippingAddress>> GetShippingAddressByUserId(string userId);
	}
}
