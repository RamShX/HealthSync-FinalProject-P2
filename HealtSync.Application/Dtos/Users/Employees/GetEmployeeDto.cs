using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Application.Dtos.Users.Employees
{
    public class GetEmployeeDto 
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public char Gender { get; set; }
        public string? JobTitle { get; set; }
        public int RoleID { get; set; }
        public int EmployeeID { get; set; }
   
    }
}
