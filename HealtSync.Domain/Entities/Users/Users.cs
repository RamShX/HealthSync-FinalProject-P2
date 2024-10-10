using HealtSync.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealtSync.Domain.Entities.Users
{
    [Table("Users", Schema = "users")]
    public class Users : ActivatableEntity
    {
        [Key]
        public int UserID { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleID {  get; set; }
    }
}
