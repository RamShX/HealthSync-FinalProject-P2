using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Persistence.Models.Medical
{
    public class SpecialityModel
    {
        public int SpecialtyID { get; set; }
        public string? SpecialtyName { get; set; }
        public bool IsActive { get; set; }
    }
}
