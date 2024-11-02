using HealtSync.Persistence.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HealtSync.Domain.Entities.Users;

namespace HealtSync.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesController(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> Get()
        {
            var result = await _employeesRepository.GetAll();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("SaveEmployee")]
        public async Task<IActionResult> Post([FromBody] Employees employee)
        {
            var result = await _employeesRepository.Save(employee);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("ModifyEmployee")]
        public async Task<IActionResult> Put([FromBody] Employees employee)
        {
            var result = await _employeesRepository.Update(employee);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("DisableEmployee")]
        public async Task<IActionResult> Delete([FromBody] Employees employee)
        {
            var result = await _employeesRepository.Remove(employee);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
