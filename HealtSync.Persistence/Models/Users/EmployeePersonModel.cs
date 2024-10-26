

namespace HealtSync.Persistence.Models.Users
{
    public class EmployeePersonModel
    {
        public int EmployeeID { get; set; }
        public int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
