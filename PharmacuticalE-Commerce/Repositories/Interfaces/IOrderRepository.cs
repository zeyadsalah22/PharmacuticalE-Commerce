using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public IEnumerable<Order> GetOrdersByUserId(string userId);
        public Order GetByIdWithDetails(int? Id);
    }
}
