
namespace HealtSync.Persistence.Models.Users
{
    public class PatientPersonModel
    {
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public string? Allergies { get; set; }
        public string? InsuranceProviderID { get; set; }
        public bool IsActive { get; set; }
    }
}
