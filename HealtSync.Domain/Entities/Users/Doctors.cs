using HealtSync.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealtSync.Domain.Entities.Users
{
    [Table("Doctors", Schema = "users")]
    public class Doctors : AuditableEntity, IContactable
    {
        [Key]
        public int DoctorID { get; set; }
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public int SpecialityID { get; set; }
        public string? Education { get; set; }
        public string? LicenseNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public  int YearsOfExperiencie { get; set; }
        public string? Bio { get; set; }
        public string? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public int AvailabilityModeId { get; set; }
        public DateTime LicenseExpirationDate { get; set; }

    }
}
