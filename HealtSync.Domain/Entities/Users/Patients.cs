using HealtSync.Domain.Base;

namespace HealtSync.Domain.Entities.Users
{
    public class Patients : ActivatableEntity
    {
        public int PatientID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public string? Allergies { get; set; }
        public string? InsuranceProviderID {  get; set; }

    }
}
