using HealtSync.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealtSync.Domain.Entities.Users
{
    [Table("Patients", Schema = "users") ]
    public class Patients : AuditableEntity, IActivatableEntity, IContactable
    {
        [Key]
        public int PatientID { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public string? Allergies { get; set; }
        public string? InsuranceProviderID {  get; set; }
        public bool IsActive { get; set; }

    }
}
