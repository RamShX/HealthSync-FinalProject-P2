using HealtSync.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealtSync.Domain.Entities.Medical
{
    [Table("AvailabilityModes", Schema = "medical")]
    public class AvailabilityModes : AuditableEntity, IActivatableEntity
    {
        [Key]
        public int AvailabilityModeID { get; set; }
        public string? AvailabilityMode { get; set; }
        public bool IsActive { get; set; }
    }
}
