using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Persistence.Models.Users
{
    internal class UserModel
    {
        public int UserID { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
    }
}
