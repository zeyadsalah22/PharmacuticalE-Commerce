using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacuticalE_Commerce.Models
{
    [ModelMetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {
    }

    public class CategoryMetaData
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string Name { get; set; } = null!;

        [ForeignKey("ParentCategory")]
        [Display(Name = "Parent Category")]
        public int? ParentCategoryId { get; set; }

        [InverseProperty("ParentCategory")]
        public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();
    }
}
