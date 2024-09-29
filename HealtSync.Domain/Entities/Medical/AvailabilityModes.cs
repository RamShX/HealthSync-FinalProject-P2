using HealtSync.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Domain.Entities.Medical
{
    internal class AvailabilityModes : ActivatableEntity
    {
        public int AvailabilityModeID { get; set; }
        public string AvailabilityMode { get; set; }}
    }
}
