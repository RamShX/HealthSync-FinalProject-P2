using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Persistence.Models.Medical
{
    internal class AvailabilityModeModel
    {
        public int AvailabilityModeID { get; set; }
        public string? AvailabilityMode { get; set; }
        public bool IsActive { get; set; }
    }
}
