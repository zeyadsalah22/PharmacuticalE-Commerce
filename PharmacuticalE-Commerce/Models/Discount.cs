using System;

namespace PharmacuticalE_Commerce.Models
{
    public partial class Discount
    {
        public int DiscountId { get; set; }

        public decimal? ValuePct { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        // Establish one-to-one relationship with Product
        public virtual Product Product { get; set; } = null!;
    }
}
