using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacuticalE_Commerce.Models
{
    [ModelMetadataType(typeof(PavMetaData))]
    public partial class Pav
    {
    }

    public class PavMetaData
    {
        [Key, Column(Order = 1)] // Composite primary key (ProductId part)
        [ForeignKey("Product")] // Foreign key reference to Product
        public int ProductId { get; set; }

        [Key, Column(Order = 2)] // Composite primary key (AttributeId part)
        [ForeignKey("Attribute")] // Foreign key reference to ProductAttribute
        public int AttributeId { get; set; }

        [Required(ErrorMessage = "Value is required")]
        [StringLength(255, ErrorMessage = "Value cannot exceed 255 characters")]
        public string Value { get; set; } = null!;

        public virtual ProductAttribute Attribute { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
