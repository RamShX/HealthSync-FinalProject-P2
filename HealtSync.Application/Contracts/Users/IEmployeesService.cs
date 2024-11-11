using HealtSync.Application.Base;
using HealtSync.Application.Dtos.Users.Employees;
using HealtSync.Application.Response.Users.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Application.Contracts.Users
{
    public interface IEmployeesService : IBaseService<EmployeesResponse, EmployeesSaveDto, EmployeesUpdateDto>
    {
    }
}
