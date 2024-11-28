using HealtSync.Application.Core;
using HealtSync.Application.Dtos.Users.Doctors;
using HealtSync.Web.Models;
using HealtSync.Web.Models.Users.Doctors;
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

        public async Task<BaseResponseModel> SaveDoctor(DoctorSaveDto doctorSaveDto)
        {
            BaseResponseModel responseModel = new();

            try
            {
                responseModel = await _httpService.PostAsync<BaseResponseModel, DoctorSaveDto>("Doctors/SaveDoctor", doctorSaveDto);
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "Ocurrió un error guardando los doctores";
                _logger.LogError($"{responseModel.Message}: {ex.ToString()}");
            }

            return responseModel;
        }

        public async Task<BaseResponseModel> UpdateDoctor(DoctorUpdateDto doctorUpdateDto)
        {

            BaseResponseModel responseModel = new();

            try
            {
                responseModel = await _httpService.PutAsync<BaseResponseModel, DoctorUpdateDto>("Doctors/UpdateDoctor", doctorUpdateDto);
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "Ocurri un error actualizando el doctor";
                _logger.LogError(ex.Message);
            }

            return responseModel;

        }


        public Task<BaseResponseModel> DisableDoctor(int id)
        {
            throw new NotImplementedException();
        }
    }
}
