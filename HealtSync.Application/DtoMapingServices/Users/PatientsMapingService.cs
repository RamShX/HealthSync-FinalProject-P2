

using HealtSync.Application.Dtos;
using HealtSync.Application.Dtos.Users;
using HealtSync.Application.Dtos.Users.Patients;
using HealtSync.Domain.Entities.Users;

namespace HealtSync.Application.DtoMapingServices.Users
{
    internal class PatientsMapingService : IDtoMappingService<(Patients, Persons, Domain.Entities.Users.Users), PatientSaveDto, PatientUpdateDto, GetPatientDto, GetDetailedPatientDto>
    {
        public GetDetailedPatientDto ConvertEntityToGetDetailedDto((Patients, Persons, Domain.Entities.Users.Users) entity)
        {
            throw new NotImplementedException();
        }

        public GetPatientDto ConvertEntityToGetSimpletDto((Patients, Persons, Domain.Entities.Users.Users) entity)
        {
            Patients patient = entity.Item1 as Patients;
            Persons person = entity.Item2 as Persons;
            var user = entity.Item3 as Domain.Entities.Users.Users;

            GetPatientDto getPatientDto = new()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,

                Address = patient.Address,
                Allergies = patient.Allergies,
                BloodType = patient.BloodType,
                ChangeDate = patient.UpdatedAt,
                EmergencyContactName = patient.EmergencyContactName,
                EmergencyContactPhone = patient.EmergencyContactPhone,
                InsuranceProviderID = patient.InsuranceProviderID,
                PhoneNumber = patient.PhoneNumber,


            };

            return getPatientDto;
        }

        public (Patients, Persons, Domain.Entities.Users.Users) ConvertSaveDtoToEntity(PatientSaveDto saveDto)
        {
            Patients patient = new()
            {
                Allergies = saveDto.Allergies,
                Address = saveDto.Address,
                BloodType = saveDto.BloodType,
                CreatedAt = DateTime.Now,
                EmergencyContactName = saveDto.EmergencyContactName,
                EmergencyContactPhone = saveDto.EmergencyContactPhone,
                InsuranceProviderID = saveDto.InsuranceProviderID,
                PhoneNumber = saveDto.PhoneNumber,

            };

            Persons person = new()
            {
                FirstName = saveDto.FirstName,
                LastName = saveDto.LastName,
                DateOfBirth = saveDto.DateOfBIrth,
                Gender = saveDto.Gender,
                IdentificationNumber = saveDto.IdentificationNumber
            };

            Domain.Entities.Users.Users users = new()
            {
                Email = saveDto.Email,
                Password = saveDto.Password,
                RoleID = saveDto.RoleID,
                CreatedAt = DateTime.Now
            };

            var patientDuple = (patient, person, users);

            return patientDuple;
        }

        public (Patients, Persons, Domain.Entities.Users.Users) ConvertUpdateDtoToEntity(PatientUpdateDto updateDto)
        {
            Patients patient = new()
            {
                PatientID = updateDto.PatientID,
                Allergies = updateDto.Allergies,
                Address = updateDto.Address,
                BloodType = updateDto.BloodType,
                CreatedAt = updateDto.ChangeDate,
                EmergencyContactName = updateDto.EmergencyContactName,
                EmergencyContactPhone = updateDto.EmergencyContactPhone,
                InsuranceProviderID = updateDto.InsuranceProviderID,
                PhoneNumber = updateDto.PhoneNumber,
                UpdatedAt = DateTime.Now

            };

            Persons person = new()
            {
                PersonID = updateDto.PatientID,
                FirstName = updateDto.FirstName,
                LastName = updateDto.LastName,
                DateOfBirth = updateDto.DateOfBIrth,
                Gender = updateDto.Gender,
                IdentificationNumber = updateDto.IdentificationNumber,
            };

            Domain.Entities.Users.Users users = new()
            {
                UserID = updateDto.PatientID,
                Email = updateDto.Email,
                Password = updateDto.Password,
                RoleID = updateDto.RoleID,
                CreatedAt = updateDto.ChangeDate,
                UpdatedAt = DateTime.Now
            };

            var patientDuple = (patient, person, users);

            return patientDuple;
        }
    }
}
