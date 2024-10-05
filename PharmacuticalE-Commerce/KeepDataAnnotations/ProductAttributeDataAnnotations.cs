using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PharmacuticalE_Commerce.Models
{
    [ModelMetadataType(typeof(ProductAttributeMetaData))]
    public partial class ProductAttribute
    {
    }

    public class ProductAttributeMetaData
    {
        [Key]
        public int AttributeId { get; set; }

        [Required(ErrorMessage = "Attribute name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Pav> Pavs { get; set; } = new List<Pav>();
    }
}
