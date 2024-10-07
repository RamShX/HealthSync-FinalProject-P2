using HealtSync.Domain.Base;

namespace HealtSync.Domain.Entities.Appointments
{
    public sealed class Appointments : AuditableEntity
    {
        public int AppointmentID { get; set; }
        public int PacientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
      
    }
}
