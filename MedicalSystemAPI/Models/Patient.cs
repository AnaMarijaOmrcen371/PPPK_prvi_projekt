using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystemAPI.Models
{
    [Index(nameof(OIB), IsUnique = true)]
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "OIB must contain exactly 11 digits.")]
        public string OIB { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ResidenceAddress { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LivingAddress { get; set; } = string.Empty;

        public List<DiseaseHistory> DiseaseHistories { get; set; } = new();

        public List<Appointment> Appointments { get; set; } = new();
    }
}