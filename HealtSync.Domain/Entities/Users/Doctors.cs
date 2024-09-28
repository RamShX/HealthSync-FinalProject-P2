using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Domain.Entities.Users
{
    internal class Doctors
    {
        public int DoctorID { get; set; }
        public int SpecialityID { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public  int YearsOfExperiencie { get; set; }
        public string? Bio { get; set; }
        public string? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public int AvailabilityModeId { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt {  get; set; }




    }
}
