namespace HealtSync.Application.Dtos.Users.Doctors
{
    public class DoctorBaseDto : UsersBaseDto
    {
        // public int DoctorID { get; set; }
        public int SpecialityID { get; set; }
        public string? Education { get; set; }
    //    public string? LicenseNumber { get; set; }
        public string? ClinicAddress { get; set; }

        public decimal ConsultationFee { get; set; }

        //  public DateTime LicenseExpirationDate { get; set; }
    }
}
