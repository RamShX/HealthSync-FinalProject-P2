﻿
namespace HealtSync.Application.Dtos.Users.Employees
{
    public class EmployeesUpdateDto : EmployeesBaseDto
    {
        public int EmployeeId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? IdentificationNumber { get; set; }
        public int RoleID { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
