

using HealtSync.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealtSync.Domain.Entities.Users
{
    [Table("Employees", Schema = "users")]
    public class Employees : AuditableEntity, IContactable, IActivatableEntity
    {
        [Key]
        public  int EmployeeID { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
