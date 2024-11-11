
namespace HealtSync.Application.Dtos.Users.Employees
{
    public class EmployeesUpdateDto : EmployeesBaseDto
    {
        public int EmployeeId { get; set; }
        public bool IsActive { get; set; }
    }
}
