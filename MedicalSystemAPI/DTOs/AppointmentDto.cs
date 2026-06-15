namespace MedicalSystemAPI.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Notes { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;

        public string DoctorName { get; set; } = string.Empty;

        public string ExaminationType { get; set; } = string.Empty;
    }
}