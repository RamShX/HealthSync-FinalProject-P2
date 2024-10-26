using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Persistence.Models.Users
{
    public class DoctorPersonModel
    {
        public int DoctorID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public int SpecialityID { get; set; }
        public string? Education { get; set; }
        public string? LicenseNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public int YearsOfExperiencie { get; set; }
        public string? Bio { get; set; }
        public string? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public int AvailabilityModeId { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
    }
}
