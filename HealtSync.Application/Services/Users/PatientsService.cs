
using HealtSync.Application.Contracts.Users;
using HealtSync.Application.DtoMapingServices.Users;
using HealtSync.Application.Dtos.Users.Patients;
using HealtSync.Application.Response.Users.Users;
using HealtSync.Domain.Entities.Users;
using HealtSync.Persistence.Interfaces.Users;
using HealtSync.Persistence.Repositories.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace HealtSync.Application.Services.Users
{
    public class PatientsService : IPatientsService
    {
        IPatientsRepository _patientsRepository;
        IUsersRepository _usersRepository;
        IPersonsRepository _personsRepository;
        ILogger<PatientsService> _logger;
        public PatientsService(IPatientsRepository patientsRepository, IUsersRepository usersRepository, IPersonsRepository personsRepository, ILogger<PatientsService> logger)
        {
            _patientsRepository = patientsRepository;
            _usersRepository = usersRepository;
            _personsRepository = personsRepository;
            _logger = logger;
        }
        public async Task<PatientResponse> DisableAsync(int id)
        {
            PatientResponse patientResponse = new();

            try
            {
                var getResult = await _patientsRepository.GetEntityBy(id);
                var patient = (Patients)getResult.Data!;

                patient.IsActive = false;

                var updateResult = await _patientsRepository.Update(patient);

                patientResponse.IsSuccess = getResult.Success && updateResult.Success;

                patientResponse.Message = patientResponse.IsSuccess ? "Se ha desactivado el paciente satisfactoriamente" : "Ha ocurrido un error desactivando el paciente";
            }
            catch (Exception ex)
            {
                patientResponse.IsSuccess = false;
                patientResponse.Message = "Ha ocurrido un error dsactivando el paciente";
                _logger.LogError(patientResponse.Message, ex);
            }

            return patientResponse;
        }

        public async Task<PatientResponse> GetAll()
        {
            PatientResponse patientResponse = new();

            try
            {
                var patientResult = await _patientsRepository.GetAll();
                var personResult = await _personsRepository.GetAll();
                var userResult = await _usersRepository.GetAll();

                var patientsList = (List<Patients>)patientResult.Data!;
                var personsList = (List<Persons>)personResult.Data!;
                var usersList = (List<Domain.Entities.Users.Users>)userResult.Data!;

                PatientsMapingService patientsMapping = new();

                List<GetPatientDto> patientsDtos = patientsList
                                                      .Select(patient =>
                                                      {
                                                          var person = personsList.FirstOrDefault(person => person.PersonID == patient.PatientID);
                                                          var user = usersList.FirstOrDefault(user => user.UserID == patient.PatientID);

                                                          var patientTuple = (patient, person, user);
                                                          GetPatientDto getPatientDto = patientsMapping.ConvertEntityToGetDto(patientTuple!);
                                                          return getPatientDto;

                                                      }).ToList();

                patientResponse.Model = patientsDtos;

                patientResponse.IsSuccess = patientResult.Success && personResult.Success & userResult.Success;
                patientResponse.Message = patientResponse.IsSuccess ? "Se ha desactivado el paciente satisfactoriamente" : string.Join(", ", patientResult.Message, personResult.Message, userResult.Message);
            }
            catch (Exception ex)
            {
                patientResponse.IsSuccess = false;
                patientResponse.Message = "Ha ocurrido un error obteniendo los pacientes.";
                _logger.LogError(patientResponse.Message, ex);
            }

            return patientResponse;
        }

        public async Task<PatientResponse> GetById(int id)
        {
            PatientResponse patientResponse = new();

            try
            {
                var patientResult = await _patientsRepository.GetEntityBy(id);
                var personResult = await _personsRepository.GetEntityBy(id);
                var userResult = await _usersRepository.GetEntityBy(id);

                var patient = (Patients)patientResult.Data!;
                var person = (Persons)personResult.Data!;
                var user = (Domain.Entities.Users.Users)userResult.Data!;

                PatientsMapingService patientesMapping = new();

                var patientTuple = (patient, person, user);

                GetPatientDto getPatientDto = patientesMapping.ConvertEntityToGetDto(patientTuple);

                patientResponse.IsSuccess = patientResult.Success && personResult.Success & userResult.Success;
                patientResponse.Message = patientResponse.IsSuccess ? "Se ha obtenido el paciente satisfactoriamente" : string.Join(", ", patientResult.Message, personResult.Message, userResult.Message);

                patientResponse.Model = getPatientDto;
            }
            catch (Exception ex)
            {
                patientResponse.IsSuccess = false;
                patientResponse.Message = "Ha ocurrido un error obteniendo el paciente";
                _logger.LogError(patientResponse.Message, ex);
            }

            return patientResponse;
        }

        public async Task<PatientResponse> SaveAsync(PatientSaveDto dto)
        {
            PatientResponse patientResponse = new();

            try
            {
                PatientsMapingService patientsMapping = new();

                var patientTuple = patientsMapping.ConvertSaveDtoToEntity(dto);

                Patients patient = patientTuple.Item1 as Patients;
                Persons person = patientTuple.Item2 as Persons;
                var user = patientTuple.Item3 as Domain.Entities.Users.Users;

                var personResult = await _personsRepository.Save(person);

                int personID = person.PersonID;

                patient.PatientID = personID;
                user.UserID = personID;

                var patientResult = await _patientsRepository.Save(patient);
                var userResult = await _usersRepository.Save(user);


                patientResponse.IsSuccess = patientResult.Success && personResult.Success & userResult.Success;
                patientResponse.Message = patientResponse.IsSuccess ? "Se ha guardado el paciente satisfactoriamente." : string.Join(", ", patientResult.Message, personResult.Message, userResult.Message);
            }
            catch (Exception ex)
            {
                patientResponse.IsSuccess = false;
                patientResponse.Message = "Ha ocurrido un error guardando el paciente";
                _logger.LogError(patientResponse.Message, ex);
            }

            return patientResponse;
        }

        public async Task<PatientResponse> UpdateAsync(PatientUpdateDto dto)
        {
            PatientResponse patientResponse = new();

            try
            {
                PatientsMapingService patientsMapping = new();

                var patientTuple = patientsMapping.ConvertUpdateDtoToEntity(dto);

                Patients patient = patientTuple.Item1 as Patients;
                Persons person = patientTuple.Item2 as Persons;
                var user = patientTuple.Item3 as Domain.Entities.Users.Users;

                var patientResult = await _patientsRepository.Update(patient);
                var personResult = await _personsRepository.Update(person);
                var userResult = await _usersRepository.Update(user);

                patientResponse.IsSuccess = patientResult.Success && personResult.Success & userResult.Success;
                patientResponse.Message = patientResponse.IsSuccess ? "Se actualizó el paciente satisfactoriamente." : string.Join(", ", patientResult.Message, personResult.Message, userResult.Message);
     
            }
            catch (Exception ex)
            {
                patientResponse.IsSuccess = false;
                patientResponse.Message = "Ha ocurrido un error actualizando el paciente";
                _logger.LogError(patientResponse.Message, ex);
            }

            return patientResponse;
        }
    }
}
