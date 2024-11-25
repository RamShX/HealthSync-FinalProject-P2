using HealtSync.Application.Dtos.Users.Doctors;
using HealtSync.Web.Models;

namespace HealtSync.Web.Services.Users
{
    public interface IDoctorApiClientService 
    {
        Task<DoctorGetAllResultModel> GetDoctors();
        Task<DoctorGetByIdResultModel> GetDoctorGetById(int id);
        Task<BaseModel> SaveDoctor(DoctorSaveDto doctorSaveDto);
        Task<BaseModel> UpdateDoctor(DoctorUpdateDto doctorUpdateDto);
        Task<BaseModel> DisableDoctor(int id);
    }
}
