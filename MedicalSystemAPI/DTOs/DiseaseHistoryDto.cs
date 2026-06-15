namespace MedicalSystemAPI.DTOs
{
    public class DiseaseHistoryDto
    {
        public int Id { get; set; }

        public string Diagnosis { get; set; } = string.Empty;

        public DateTime DateRecorded { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string DoctorName { get; set; } = string.Empty;
    }
}