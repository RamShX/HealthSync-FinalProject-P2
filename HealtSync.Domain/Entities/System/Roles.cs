using HealtSync.Domain.Base;

namespace HealtSync.Domain.Entities.System
{
    public class Roles : ActivatableEntity
    {
        public int RoleID { get; set; }
        public string? RoleName { get; set; }
 
    }
}
