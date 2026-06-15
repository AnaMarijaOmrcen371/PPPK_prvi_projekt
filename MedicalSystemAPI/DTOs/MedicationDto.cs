namespace MedicalSystemAPI.DTOs
{
    public class MedicationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Dose { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
    }
}
