

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealtSync.Domain.Entities.Users
{
    [Table("Persons", Schema = "users")]
    public class Persons
    {
        [Key]
        public int PersonID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? IdentificationNumber { get; set; }
        public char Gender { get; set; }

    }
}
