using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSystemAPI.Models
{
    public class DiseaseHistory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Diagnosis { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime DateRecorded { get; set; }

        [Required]
        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        [Required]
        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set; }
    }
}