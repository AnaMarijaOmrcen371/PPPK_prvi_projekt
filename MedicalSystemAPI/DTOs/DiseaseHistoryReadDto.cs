namespace MedicalSystemAPI.DTOs
{
    public class DiseaseHistoryReadDto
    {
        public int Id { get; set; }

        public string Diagnosis { get; set; } = string.Empty;

        public string DateRecorded { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;

        public string DoctorName { get; set; } = string.Empty;
    }
}