
using HealtSync.Application.Contracts.Users;
using HealtSync.Application.DtoMapingServices.Users;
using HealtSync.Application.Dtos.Users.Doctors;
using HealtSync.Application.Response.Users.Employees;
using HealtSync.Application.Response.Users.Users;
using HealtSync.Domain.Entities.Users;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.Extensions.Logging;

namespace HealtSync.Application.Services.Users
{
    public class DoctorsService : IDoctorsService
    {

        private readonly IDoctorsRepository _doctorsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IPersonsRepository _personsRepository;
        private readonly ILogger<DoctorsService> _logger;
       

        public DoctorsService(IDoctorsRepository doctorsRepository, IPersonsRepository personsRepository, IUsersRepository usersRepository, ILogger<DoctorsService> logger)
        {
            _doctorsRepository = doctorsRepository;
            _usersRepository = usersRepository;
            _personsRepository = personsRepository;
            _logger = logger;
        }
        public async Task<DoctorResponse> DisableAsync(int id)
        {

            DoctorResponse doctorResponse = new();

            try
            {
                var getResult = await _doctorsRepository.GetEntityBy(id);
                Doctors doctor = getResult.Data!;

                doctor.IsActive = false;
                var updateResult = await _doctorsRepository.Update(doctor);

                doctorResponse.IsSuccess = updateResult.Success && getResult.Success;

                doctorResponse.Message = doctorResponse.IsSuccess ? "Se ha desactivado el doctor exitosamente" : "Hubo un error desactivando el doctor";
            }
            catch (Exception ex)
            {
                doctorResponse.IsSuccess = false;
                doctorResponse.Message = "Hubo un error desactivando el doctor";
                _logger.LogError(doctorResponse.Message, ex);
            }

            return doctorResponse;
        }

        public async Task<DoctorResponse> GetAll()
        {
            DoctorResponse doctorResponse = new();

            try
            {
                var doctorResult = await _doctorsRepository.GetAll();
                var personResult = await _personsRepository.GetAll();
                var userResult = await _usersRepository.GetAll();

                var doctorsList = (List<Doctors>)doctorResult.Data!;
                var personsList = (List<Persons>)personResult.Data!;
                var usersList = (List<Domain.Entities.Users.Users>)userResult.Data!;

                DoctorMappingService doctorMapping = new();

                List<GetDoctorDto> doctors = doctorsList
                                              .Select(doctor =>
                                              {
                                                  var person = personsList.FirstOrDefault(person => person.PersonID == doctor.DoctorID);
                                                  var user = usersList.FirstOrDefault(user => user.UserID == doctor.DoctorID);

                                                  var doctorTuple = (doctor, person, user);

                                                  GetDoctorDto getDoctorDto = doctorMapping.EntityToGetDto(doctorTuple!);

                                                  return getDoctorDto;

                                              }).ToList();

                doctorResponse.IsSuccess = doctorResult.Success && personResult.Success && userResult.Success;
                doctorResponse.Model = doctors;


            }
            catch (Exception ex)
            {
                doctorResponse.IsSuccess = false;
                doctorResponse.Message = "Ocurrió un error obteniendo los doctores";
                _logger.LogError(doctorResponse.Message, ex);
            } 
            
            return doctorResponse;
        }

        public Task<DoctorResponse> GetByAvailabilityDate(DateTime? availabilityDate)
        {
            throw new NotImplementedException();
        }

        public async Task<DoctorResponse> GetById(int id)
        {
            DoctorResponse doctorResponse = new();

            try
            {
                var doctorResult = await _doctorsRepository.GetEntityBy(id);
                var personResult = await _personsRepository.GetEntityBy(id);
                var userResult = await _usersRepository.GetEntityBy(id);

                Doctors doctor = doctorResult.Data!;
                Persons person = personResult.Data!;
                Domain.Entities.Users.Users user = userResult.Data!;

                var doctorTuple = (doctor, person, user);
                

                DoctorMappingService doctorMapping = new();

                GetDoctorDto getDoctorDto = doctorMapping.EntityToGetDto(doctorTuple);

                doctorResponse.IsSuccess = doctorResult.Success && personResult.Success && userResult.Success;

                doctorResponse.Model = getDoctorDto; 


            }
            catch(Exception ex)
            {
                doctorResponse.IsSuccess = false;
                doctorResponse.Message = "Ocurrió un error obteniendo el doctor";
                _logger.LogError(doctorResponse.Message, ex);
            }

            return doctorResponse;
        }

        public async Task<DoctorResponse> SaveAsync(DoctorSaveDto dto)
        {
            DoctorResponse doctorResponse = new DoctorResponse();

            try
            {
                DoctorMappingService doctorMapping = new();
               
                var doctorTuple = doctorMapping.SaveDtoToEntity(dto);

                Doctors doctor = doctorTuple.Item1;
                Persons person = doctorTuple.Item2;
                Domain.Entities.Users.Users user = doctorTuple.Item3;

                var savePersonResult = await _personsRepository.Save(person);

                int personID = person.PersonID;

                doctor.DoctorID = personID;
                user.UserID = personID;

                var saveDoctorResult = await _doctorsRepository.Save(doctor);
                var saveUserResult = await _usersRepository.Save(user);

                doctorResponse.IsSuccess = saveDoctorResult.Success && savePersonResult.Success && saveUserResult.Success;

                doctorResponse.Message = doctorResponse.IsSuccess ? "El doctor se ha guardado correctamente." : "Hubo un error guardando el doctor";

            }
            catch(Exception ex)
            {
                doctorResponse.IsSuccess = false;
                doctorResponse.Message = "Hubo un error guardando el doctor";
                _logger.LogError(doctorResponse.Message, ex);
            }

            return doctorResponse;
        }

        public async Task<DoctorResponse> UpdateAsync(DoctorUpdateDto dto)
        {
            DoctorResponse doctorResponse = new DoctorResponse();

            try
            {
                DoctorMappingService doctorMapping = new();

                var doctorTuple = doctorMapping.UpdateDtoToEntity(dto);

                Doctors doctor = doctorTuple.Item1;
                Persons person = doctorTuple.Item2;
                Domain.Entities.Users.Users user = doctorTuple.Item3;

                var updatePersonResult = await _personsRepository.Update(person);
                var updateDoctorResult = await _doctorsRepository.Update(doctor);
                var updateUserResult = await _usersRepository.Update(user);

                doctorResponse.IsSuccess = updateDoctorResult.Success && updatePersonResult.Success && updateUserResult.Success;

                doctorResponse.Message = doctorResponse.IsSuccess ? "El doctor se ha actualizado correctamente." : "Hubo un error actualizando el doctor";

            }
            catch (Exception ex)
            {
                doctorResponse.IsSuccess = false;
                doctorResponse.Message = "Hubo un error actualizando el doctor";
                _logger.LogError(doctorResponse.Message, ex);
            }

            return doctorResponse;
        }
    }
}
