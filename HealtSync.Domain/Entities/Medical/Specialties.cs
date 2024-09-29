using HealtSync.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Domain.Entities.Medical
{
    internal class Specialties: ActivatableEntity
    {
        public int SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }


    }
}
