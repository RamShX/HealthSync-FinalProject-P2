

namespace HealtSync.Domain.Entities.Appointments
{
    public sealed class DoctorAvailability
    {
        public int AvailabilityID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeOnly Startime { get; set; }
        public TimeOnly EndTime { get; set; }

    }
}
