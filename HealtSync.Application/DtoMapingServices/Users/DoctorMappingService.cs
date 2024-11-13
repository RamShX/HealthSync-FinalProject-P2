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
    public class DoctorMappingService : IDtoMappingService<(Doctors, Persons, Domain.Entities.Users.Users), DoctorSaveDto, DoctorUpdateDto, GetSimpleDoctorDto, GetDetailedDoctorDto>
    {
        public GetDetailedDoctorDto ConvertEntityToGetDetailedDto((Doctors, Persons, Domain.Entities.Users.Users) entity)
        {
            Doctors doctor = entity.Item1;
            Persons person = entity.Item2;
            var user = entity.Item3;

            GetDetailedDoctorDto getDetailedDoctorDto = new GetDetailedDoctorDto()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth,
                IdentificationNumber = person.IdentificationNumber,
                

                DoctorID = doctor.DoctorID,
                ChangeDate = doctor.UpdatedAt,
                ClinicAddress = doctor.ClinicAddress,
                ConsultationFee = doctor.ConsultationFee,
                SpecialityID = doctor.SpecialtyID,
                Education = doctor.Education,
                LicenseNumber = doctor.LicenseNumber,
                LicenseExpirationDate = doctor.LicenseExpirationDate,
                AvailabilityModeId = doctor.AvailabilityModeId,
                Bio     = doctor.Bio,   
                YearsOfExperiencie = doctor.YearsOfExperience,
                PhoneNumber = doctor.PhoneNumber,
                
                Email = user.Email,
                Password = user.Password,
                RoleID = user.RoleID,       

            };

            return getDetailedDoctorDto;
        }

        public GetSimpleDoctorDto ConvertEntityToGetSimpletDto((Doctors, Persons, Domain.Entities.Users.Users) entity)
        {
            Doctors doctor = entity.Item1;
            Persons person = entity.Item2;
            var user = entity.Item3;

            GetSimpleDoctorDto getDoctorDto = new GetSimpleDoctorDto()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,  

                DoctorID = doctor.DoctorID,
                ChangeDate = doctor.UpdatedAt,
                ClinicAddress = doctor.ClinicAddress,
                ConsultationFee = doctor.ConsultationFee,
                SpecialityID = doctor.SpecialtyID,
                Education = doctor.Education,

            };

            return getDoctorDto;
        }

        public (Doctors, Persons, Domain.Entities.Users.Users) ConvertSaveDtoToEntity(DoctorSaveDto saveDto)
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
                SpecialtyID = saveDto.SpecialityID,
                YearsOfExperience = saveDto.YearsOfExperiencie
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
                CreatedAt = saveDto.ChangeDate

            };

            var doctorTuple = (doctor, person, user);

            return doctorTuple;

        }

        public (Doctors, Persons, Domain.Entities.Users.Users) ConvertUpdateDtoToEntity(DoctorUpdateDto updateDto)
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
                SpecialtyID = updateDto.SpecialityID,
                YearsOfExperience = updateDto.YearsOfExperiencie
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
                CreatedAt = updateDto.ChangeDate


            };

            var doctorTuple = (doctor, person, user);

            return doctorTuple;
        }
    }
}

