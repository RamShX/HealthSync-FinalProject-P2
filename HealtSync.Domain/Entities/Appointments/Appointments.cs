using HealtSync.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HealtSync.Domain.Entities.Appointments
{
    [Table("Appointments", Schema = "appointments")]
    public sealed class Appointments : AuditableEntity
    {
        [Key]
        public int AppointmentID { get; set; }
        public int PacientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
      
    }
}
