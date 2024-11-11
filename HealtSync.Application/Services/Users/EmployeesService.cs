using HealtSync.Application.Base;
using HealtSync.Application.Dtos.Users.Employees;
using HealtSync.Application.Response.Users.Employees;
using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Repositories.Users;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Application.Services.Users
{
    public class EmployeesService : IBaseService<EmployeesResponse, EmployeesSaveDto, EmployeesUpdateDto>
    {
        private readonly EmployeesRepository _employeesRepository;
        private readonly PersonsRepository _personsRepository;
        private readonly UsersRepository _usersRepository;
        private readonly ILogger<EmployeesService> _logger;

        public EmployeesService(EmployeesRepository employeesRepository, PersonsRepository personsRepository, UsersRepository usersRepository, ILogger<EmployeesService> logger)
        {
            if (employeesRepository is null)
                throw new ArgumentNullException(nameof(employeesRepository));

            _employeesRepository = employeesRepository;
            _personsRepository = personsRepository;
            _usersRepository = usersRepository;
            _logger = logger;

        }
        public async Task<EmployeesResponse> GetAll()
        {
            EmployeesResponse employeesResponse = new();

            try
            {
                var employeesResult = await _employeesRepository.GetAll();
                var personsResult = await _personsRepository.GetAll();
                var usersResult = await _usersRepository.GetAll();

                var employeesList = (List<Employees>)employeesResult.Data!;
                var personsList = (List<Persons>)personsResult.Data!;
                var usersList = (List<Domain.Entities.Users.Users>)usersResult.Data!;


                List<GetEmployeeDto> employees = employeesList
                                                .Select(employee => new GetEmployeeDto()
                                                {
                                                    EmployeeID = employee.EmployeeID,
                                                    JobTitle = employee.JobTitle,
                                                    ChangeDate = employee.UpdatedAt,
                                                    FirstName = personsList.FirstOrDefault(person => person.PersonID == employee.EmployeeID)!.FirstName,
                                                    LastName = personsList.FirstOrDefault(person => person.PersonID == employee.EmployeeID)!.LastName,
                                                    IdentificationNumber = personsList.FirstOrDefault(person => person.PersonID == employee.EmployeeID)!.IdentificationNumber,
                                                    Gender = personsList.FirstOrDefault(person => person.PersonID == employee.EmployeeID)!.Gender,
                                                    RoleID = usersList.FirstOrDefault(user => user.UserID == employee.EmployeeID)!.UserID
                                                }).ToList();

                employeesResponse.IsSuccess = employeesResult.Success && personsResult.Success && usersResult.Success;
                employeesResponse.model = employees;

            }
            catch (Exception ex)
            {
                employeesResponse.IsSuccess = false;
                employeesResponse.Message = "Hubo un error obtienendo los empleados";
                _logger.LogError(employeesResponse.Message, ex);
            }
            return employeesResponse;


        }

        public async Task<EmployeesResponse> GetById(int id)
        {
            EmployeesResponse employeesResponse = new();

            try
            {
                var employeeResult = await _employeesRepository.GetEntityBy(id);
                var userResult = await _usersRepository.GetEntityBy(id);
                var personResult = await _personsRepository.GetEntityBy(id);

                Employees employee = employeeResult.Data!;
                Persons person = personResult.Data!;
                Domain.Entities.Users.Users user = userResult.Data!;

                GetEmployeeDto employeeDto = new GetEmployeeDto()
                {
                    EmployeeID = employee.EmployeeID,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    IdentificationNumber = person.IdentificationNumber,
                    Gender = person.Gender,
                    JobTitle = employee.JobTitle,
                    RoleID = user.RoleID,
                    ChangeDate = employee.UpdatedAt

                };

                employeesResponse.IsSuccess = employeeResult.Success && userResult.Success && personResult.Success;
                employeesResponse.model = employeeDto;

            }
            catch (Exception ex)
            {
                employeesResponse.IsSuccess = false;
                employeesResponse.Message = "Ocurrió un error obteniendon el empleado";
                _logger.LogError(employeesResponse.Message, ex);
            }

            return employeesResponse;

        }

        public async Task<EmployeesResponse> SaveAsync(EmployeesSaveDto dto)
        {
            EmployeesResponse employeesResponse = new EmployeesResponse();

            try
            {
                Persons person = new Persons()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    IdentificationNumber = dto.IdentificationNumber,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth
                };

                var personResult = await _personsRepository.Save(person);
                int personID = person.PersonID;

                Employees employee = new()
                {
                    EmployeeID = personID,
                    JobTitle = dto.JobTitle,
                    PhoneNumber = dto.PhoneNumber,
                    CreatedAt = dto.ChangeDate,

                };

                Domain.Entities.Users.Users user = new()
                {
                    UserID = personID,
                    Email = dto.Email,
                    Password = dto.Password,
                    RoleID = dto.RoleID,

                };

                var userResult = await _usersRepository.Save(user);
                var employeeResult = await _employeesRepository.Save(employee);

                employeesResponse.IsSuccess = userResult.Success && employeeResult.Success && personResult.Success;

            }
            catch (Exception ex)
            {
                employeesResponse.IsSuccess = false;
                employeesResponse.Message = "Ocurrió un error obteniendon el empleado";
                _logger.LogError(employeesResponse.Message, ex);
            }

            return employeesResponse;
        }

        public async Task<EmployeesResponse> UpdateAsync(EmployeesUpdateDto dto)
        {
            EmployeesResponse employeeResponse = new();

            try
            {
                var resultEmployee = await _employeesRepository.GetEntityBy(dto.EmployeeId);
                var resultPerson = await _personsRepository.GetEntityBy(dto.EmployeeId);
                var resultUser = await _usersRepository.GetEntityBy(dto.EmployeeId);

                Employees employeeToUpdate = resultEmployee.Data!;
                Persons personToUpdate = resultEmployee.Data!;
                Domain.Entities.Users.Users userToUpdate = resultUser.Data!;

                personToUpdate.FirstName = dto.FirstName;
                personToUpdate.LastName = dto.LastName;
                personToUpdate.DateOfBirth = dto.DateOfBirth;
                personToUpdate.IdentificationNumber = dto.IdentificationNumber;
                personToUpdate.Gender = dto.Gender; 
                
                employeeToUpdate.JobTitle = dto.JobTitle;
                employeeToUpdate.UpdatedAt = dto.ChangeDate;
                employeeToUpdate.PhoneNumber = dto.PhoneNumber;
                
                userToUpdate.Email = dto.Email;
                userToUpdate.Password = dto.Password;
                userToUpdate.RoleID = dto.RoleId;

                var resultUpdateEmployee = await _employeesRepository.Update(employeeToUpdate);
                var resultUpdateUser = await _usersRepository.Update(userToUpdate);
                var resultUpdatePerson = await _personsRepository.Update(personToUpdate);

                employeeResponse.IsSuccess = resultUpdateEmployee.Success && resultUpdateEmployee.Success && resultUpdateUser.Success;

            }
            catch(Exception ex)
            {
                employeeResponse.IsSuccess = false;
                employeeResponse.Message = "Hubo un error actualizando el empleado";
                _logger.LogError(employeeResponse.Message, ex);
                
            }

            return employeeResponse;
;
        }
    }
}
