using HealtSync.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Domain.Entities.System
{
    internal class Roles : ActivatableEntity
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
 
    }
}
