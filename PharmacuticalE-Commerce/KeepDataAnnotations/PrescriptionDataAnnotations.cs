using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacuticalE_Commerce.Models
{
    [ModelMetadataType(typeof(PrescriptionMetaData))]
    public partial class Prescription
    {
    }


    public class PrescriptionMetaData
    {
        [Key]
        [ForeignKey("Cart")]
        public int PrescriptionId { get; set; }

        public string Status { get; set; } = null!;

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string Photo { get; set; } = null!;

        public virtual Cart Cart { get; set; } = null!;

        public virtual Employee? Employee { get; set; } = null!;

        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
