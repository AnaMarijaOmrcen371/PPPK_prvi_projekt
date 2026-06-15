namespace MedicalSystemAPI.DTOs
{
    public class AppointmentReadDto
    {
        public int Id { get; set; }

        public string AppointmentDate { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public string ExaminationType { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;

        public string DoctorName { get; set; } = string.Empty;
    }
}