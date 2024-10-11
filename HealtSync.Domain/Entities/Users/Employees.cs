

using HealtSync.Domain.Base;

namespace HealtSync.Domain.Entities.Users
{
    internal class Employees : IContactable
    {
        public  int EmployeeID { get; set; }
        public int UserID { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobTitle { get; set; }
        
    }
}
