using HealtSync.Application.Dtos.Users.Doctors;
using HealtSync.Web.Models;
using HealtSync.Web.Models.Users.Doctors;

namespace HealtSync.Web.Services.Users
{
    public interface IDoctorApiClientService 
    {
        Task<DoctorGetAllResultModel> GetDoctors();
        Task<DoctorGetByIdResultModel> GetDoctorGetById(int id);
        Task<BaseResponseModel> SaveDoctor(DoctorSaveDto doctorSaveDto);
        Task<BaseResponseModel> UpdateDoctor(DoctorUpdateDto doctorUpdateDto);
        Task<BaseResponseModel> DisableDoctor(int id);
    }
}
