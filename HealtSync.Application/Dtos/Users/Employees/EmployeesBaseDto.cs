namespace HealtSync.Application.Dtos.Users.Employees
{
    public class EmployeesBaseDto : UsersBaseDto
    {
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string? JobTitle { get; set; }

    }
}
