using HealtSync.Application.Base;
using HealtSync.Application.Dtos.Users.Employees;
using HealtSync.Application.Response.Users.Employees;


namespace HealtSync.Application.Contracts.Users
{
    public interface IEmployeesService : IBaseService<EmployeesResponse, EmployeeSaveDto, EmployeeUpdateDto>
    {
    }
}
