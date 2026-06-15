namespace MedicalSystemAPI.DTOs
{
    public class PatientReadDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string OIB { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public string ResidenceAddress { get; set; } = string.Empty;
    }
}