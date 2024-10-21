using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface IDiscountRepository : IRepository<Discount>
	{
		Task<Discount> GetByIdWithProduct(int? id);
	}
}
