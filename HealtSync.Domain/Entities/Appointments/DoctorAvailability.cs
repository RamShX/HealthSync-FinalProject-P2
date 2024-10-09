

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealtSync.Domain.Entities.Appointments
{
    [Table("DoctorAvailability", Schema = "appointment")]
    public sealed class DoctorAvailability
    {
        [Key]
        public int AvailabilityID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeOnly Startime { get; set; }
        public TimeOnly EndTime { get; set; }

    }
}
