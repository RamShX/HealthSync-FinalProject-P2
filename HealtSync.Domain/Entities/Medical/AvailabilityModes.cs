using HealtSync.Domain.Base;

namespace HealtSync.Domain.Entities.Medical
{
    public class AvailabilityModes : ActivatableEntity
    {
        public int AvailabilityModeID { get; set; }
        public string? AvailabilityMode { get; set; }
    }
}
