using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
		Task<Order> GetByIdWithDetails(int? Id);
	}
}
