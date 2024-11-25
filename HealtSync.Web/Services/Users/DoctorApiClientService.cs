using HealtSync.Application.Core;
using HealtSync.Application.Dtos.Users.Doctors;
using HealtSync.Web.Models;
using HealtSync.Web.Services.Base;

namespace HealtSync.Web.Services.Users
{
    public class DoctorApiClientService : IDoctorApiClientService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<DoctorApiClientService> _logger;
        private readonly IConfiguration _configuration;

        public DoctorApiClientService(IHttpService httpService, ILogger<DoctorApiClientService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }
        public async Task<DoctorGetAllResultModel> GetDoctors()
        {
            DoctorGetAllResultModel doctorGetAllResultModel = new DoctorGetAllResultModel();

            try
            {
                doctorGetAllResultModel = await _httpService.GetAsync<DoctorGetAllResultModel>("Doctors/GetDoctors");
            }
            catch (Exception ex)
            {
                doctorGetAllResultModel.IsSuccess = false;
                doctorGetAllResultModel.Message = "Ocurrió un error obteniendo los doctores";
                _logger.LogError($"{doctorGetAllResultModel.Message}: {ex.ToString()}"); 
            }

            return doctorGetAllResultModel;
        }
        public async Task<DoctorGetByIdResultModel> GetDoctorGetById(int id)
        {
            DoctorGetByIdResultModel doctorGetByIdResultModel = new DoctorGetByIdResultModel();

            try
            {
                doctorGetByIdResultModel = await _httpService.GetAsync<DoctorGetByIdResultModel>($"Doctors/GetDoctorById?id={id}");
            }
            catch (Exception ex)
            {
                doctorGetByIdResultModel.IsSuccess = false;
                doctorGetByIdResultModel.Message = "Ocurrió un error obteniendo los doctores";
                _logger.LogError($"{doctorGetByIdResultModel.Message}: {ex.ToString()}");
            }

            return doctorGetByIdResultModel;
        }

        public async Task<BaseModel> SaveDoctor(DoctorSaveDto doctorSaveDto)
        {
            BaseModel resultModel = new();

            try
            {
                resultModel = await _httpService.PostAsync<BaseModel>("Doctors/SaveDoctor", doctorSaveDto);
            }
            catch (Exception ex)
            {
                doctorGetByIdResultModel.IsSuccess = false;
                doctorGetByIdResultModel.Message = "Ocurrió un error obteniendo los doctores";
                _logger.LogError($"{doctorGetByIdResultModel.Message}: {ex.ToString()}");
            }

            return doctorGetByIdResultModel;
        }

        public Task<BaseModel> UpdateDoctor(DoctorUpdateDto doctorUpdateDto)
        {

            throw new NotImplementedException();
        }

        public Task<BaseModel> DisableDoctor()
        {
            throw new NotImplementedException();
        }

        

    }
}
