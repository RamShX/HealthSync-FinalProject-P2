using HealtSync.Application.Dtos;
using HealtSync.Application.Dtos.Users.Doctors;
using HealtSync.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Application.DtoMapingServices.Users
{
    public class DoctorMappingService : IDtoMapingService<(Doctors, Persons, Domain.Entities.Users.Users), DoctorSaveDto, DoctorUpdateDto, GetDoctorDto>
    {
        public GetDoctorDto EntityToGetDto((Doctors, Persons, Domain.Entities.Users.Users) entity)
        {
            Doctors doctor = entity.Item1;
            Persons person = entity.Item2;
            var user = entity.Item3;

            GetDoctorDto getDoctorDto = new GetDoctorDto()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,

                Email = user.Email,

                AvailabilityModeId = doctor.AvailabilityModeId,
                Bio = doctor.Bio,
                ChangeDate = doctor.UpdatedAt,
                ClinicAddress = doctor.ClinicAddress,
                ConsultationFee = doctor.ConsultationFee,
                SpecialityID = doctor.SpecialityID,
                Education = doctor.Education,
                PhoneNumber = doctor.PhoneNumber,
                YearsOfExperiencie = doctor.YearsOfExperiencie

            };

            return getDoctorDto;
        }

        public (Doctors, Persons, Domain.Entities.Users.Users) SaveDtoToEntity(DoctorSaveDto saveDto)
        {
            Doctors doctor = new Doctors()
            {
                AvailabilityModeId = saveDto.AvailabilityModeId,
                Bio = saveDto.Bio,
                CreatedAt = saveDto.ChangeDate,
                ClinicAddress = saveDto.ClinicAddress,
                ConsultationFee = saveDto.ConsultationFee,
                Education = saveDto.Education,
                LicenseExpirationDate = saveDto.LicenseExpirationDate,
                LicenseNumber = saveDto.LicenseNumber,
                PhoneNumber = saveDto.PhoneNumber,
                SpecialityID = saveDto.SpecialityID,
                YearsOfExperiencie = saveDto.YearsOfExperiencie
            };

            Persons person = new Persons()
            {
                FirstName = saveDto.FirstName,
                LastName = saveDto.LastName,
                DateOfBirth = saveDto.DateOfBIrth,
                Gender = saveDto.Gender,
                IdentificationNumber = saveDto.IdentificationNumber
            };

            Domain.Entities.Users.Users user = new()
            {
                Email = saveDto.Email,
                Password = saveDto.Password,
                RoleID = saveDto.RoleID,

            };

            var doctorTuple = (doctor, person, user);

            return doctorTuple;

        }

        public (Doctors, Persons, Domain.Entities.Users.Users) UpdateDtoToEntity(DoctorUpdateDto updateDto)
        {
            Doctors doctor = new Doctors()
            {   
                DoctorID = updateDto.DoctorID,
                AvailabilityModeId = updateDto.AvailabilityModeId,
                Bio = updateDto.Bio,
                CreatedAt = updateDto.ChangeDate,
                ClinicAddress = updateDto.ClinicAddress,
                ConsultationFee = updateDto.ConsultationFee,
                Education = updateDto.Education,
                LicenseExpirationDate = updateDto.LicenseExpirationDate,
                LicenseNumber = updateDto.LicenseNumber,
                PhoneNumber = updateDto.PhoneNumber,
                SpecialityID = updateDto.SpecialityID,
                YearsOfExperiencie = updateDto.YearsOfExperiencie
            };

            Persons person = new Persons()
            {
                PersonID = updateDto.DoctorID,
                FirstName = updateDto.FirstName,
                LastName = updateDto.LastName,
                DateOfBirth = updateDto.DateOfBirth,
                Gender = updateDto.Gender,
                IdentificationNumber = updateDto.IdentificationNumber
            };

            Domain.Entities.Users.Users user = new()
            {
                UserID = updateDto.DoctorID,
                Email = updateDto.Email,
                Password = updateDto.Password,
                RoleID = updateDto.RoleID,

            };

            var doctorTuple = (doctor, person, user);

            return doctorTuple;
        }
    }
}

