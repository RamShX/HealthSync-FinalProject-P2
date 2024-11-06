using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Application.Dtos.Users.Persons
{
    public class UsersBaseDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleID { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
