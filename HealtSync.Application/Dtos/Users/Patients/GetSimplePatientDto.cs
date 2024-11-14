
namespace HealtSync.Application.Dtos.Users.Patients
{
    public class GetSimplePatientDto : UsersBaseDto
    {
        //Nombre, apellido, genero, ID
        public int PatientID { get; set; }
    }
}
