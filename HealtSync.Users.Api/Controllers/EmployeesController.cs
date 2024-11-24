using HealtSync.Persistence.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HealtSync.Domain.Entities.Users;
using HealtSync.Application.Contracts.Users;
using HealtSync.Application.Dtos.Users.Employees;

namespace HealtSync.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> Get()
        {
            var result = await _employeesService.GetAll();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetEmployeeById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _employeesService.GetById(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("SaveEmployee")]
        public async Task<IActionResult> Post([FromBody ]EmployeeSaveDto employeeSaveDto)
        {
            var result = await _employeesService.SaveAsync(employeeSaveDto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("ModifyEmployee")]
        public async Task<IActionResult> Put([FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            var result = await _employeesService.UpdateAsync(employeeUpdateDto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        
        [HttpDelete("DisableEmployee")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _employeesService.DisableAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
