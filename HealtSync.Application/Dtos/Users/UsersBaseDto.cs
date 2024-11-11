

using System.Reflection.Metadata.Ecma335;

namespace HealtSync.Application.Dtos.Users
{
    public abstract class UsersBaseDto : BaseDto
    {
        //Esta es la entidad base que contendrá los datos correspondientes a las entiddades persons y users

        //Correspondiente a Person
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? IdentificationNumber { get; set; }
        public char Gender { get; set; }

        //Correspondiente a User
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleID { get; set; }

    }
}
