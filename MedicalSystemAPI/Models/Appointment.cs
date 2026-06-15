using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSystemAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(20)]
        public string ExaminationType { get; set; } = string.Empty;

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        [Required]
        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        [Required]
        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set; }
    }
}