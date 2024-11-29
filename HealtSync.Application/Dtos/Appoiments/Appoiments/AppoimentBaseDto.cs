
namespace HealtSync.Application.Dtos.Appoiments.Appoiments
{
    public class AppoimentBaseDto
    {
        public string Patient { get; set; }
        public string Doctor { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
    }
}
