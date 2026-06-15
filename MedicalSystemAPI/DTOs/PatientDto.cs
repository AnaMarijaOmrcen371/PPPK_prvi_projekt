namespace MedicalSystemAPI.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Oib { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string ResidenceAddress { get; set; } = string.Empty;

        public string LivingAddress { get; set; } = string.Empty;
    }
}