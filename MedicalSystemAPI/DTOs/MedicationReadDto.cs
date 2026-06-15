namespace MedicalSystemAPI.DTOs
{
    public class MedicationReadDto
    {
        public int Id { get; set; }

        public string MedicationName { get; set; } = string.Empty;

        public string Dosage { get; set; } = string.Empty;

        public string Frequency { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;
    }
}