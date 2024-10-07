using HealtSync.Domain.Base;

namespace HealtSync.Domain.Entities.Users
{
    public class Doctors : AuditableEntity
    {
        public int DoctorID { get; set; }
        public int SpecialityID { get; set; }
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
