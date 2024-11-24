
namespace HealtSync.Application.Dtos.Users.Employees
{
    public class EmployeeSaveDto : EmployeeBaseDto
    {
        public DateTime DateOfBirth { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Password { get; set; }
        public int RoleID { get; set; }
        public string? Email { get; set; }
    }
}
