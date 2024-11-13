
using HealtSync.Application.Base;
using HealtSync.Application.Dtos.Users.Patients;
using HealtSync.Application.Response.Users.Users;

namespace HealtSync.Application.Contracts.Users
{
    public interface IPatientsService : IBaseService<PatientResponse, PatientSaveDto, PatientUpdateDto>
    {

    }
}
