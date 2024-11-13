
namespace HealtSync.Application.Dtos.Users.Doctors
{
    public class DoctorSaveDto : DoctorBaseDto
    {
        public string? LicenseNumber { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public DateTime DateOfBIrth { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Password { get; set; }
        public int RoleID { get; set; }
        public int YearsOfExperiencie { get; set; }
        public string? Bio { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public int AvailabilityModeId { get; set; }

    }
}
