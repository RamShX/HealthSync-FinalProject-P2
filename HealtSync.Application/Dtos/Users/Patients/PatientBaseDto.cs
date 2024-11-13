
namespace HealtSync.Application.Dtos.Users.Patients
{
    public class PatientBaseDto : UsersBaseDto
    {
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public string? Allergies { get; set; }
        public int InsuranceProviderID { get; set; }
    }
}
