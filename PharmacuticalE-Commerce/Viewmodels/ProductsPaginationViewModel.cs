using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Viewmodels
{
    public class ProductsPaginationViewModel
    {
        public List<Product> Products { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalRecords, PageSize));
    }
}
