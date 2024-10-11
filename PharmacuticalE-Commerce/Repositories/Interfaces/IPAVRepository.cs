using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IPAVRepository : IRepository<Pav>
    {
        public Pav GetByCompId(int? productId, int? attributeId);
        public void DeleteByCompId(int? productId, int? attributeId);
    }
}
