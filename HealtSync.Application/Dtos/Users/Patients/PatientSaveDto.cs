
namespace HealtSync.Application.Dtos.Users.Patients
{
    public class PatientSaveDto : PatientBaseDto
    {
        public string? IdentificationNumber { get; set; }
        public DateTime DateOfBIrth { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int RoleID { get; set; }

    }
}
