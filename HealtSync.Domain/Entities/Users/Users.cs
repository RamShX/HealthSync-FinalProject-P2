using HealtSync.Domain.Base;

namespace HealtSync.Domain.Entities.Users
{
    public class Users : ActivatableEntity
    {
        public int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleID {  get; set; }
    }
}
