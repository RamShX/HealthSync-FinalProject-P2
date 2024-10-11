using HealtSync.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealtSync.Domain.Entities.System
{
    [Table("Roles", Schema = "system")]
    public class Roles : AuditableEntity, IActivatableEntity
    {
        [Key]
        public int RoleID { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }

    }
}
