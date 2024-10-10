

namespace HealtSync.Domain.Entities.Users
{
     class Persons
    {
        public int PersonID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? IdentificationNumber { get; set; }  
        public char Gender { get; set; }

    }
}
