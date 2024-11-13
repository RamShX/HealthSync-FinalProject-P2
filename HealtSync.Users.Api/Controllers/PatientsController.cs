using HealtSync.Persistence.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HealtSync.Domain.Entities.Users;
using HealtSync.Application.Contracts.Users;
using HealtSync.Application.Dtos.Users.Patients;

namespace HealtSync.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;

        public PatientsController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        [HttpGet("GetPatients")]
        public async Task<IActionResult> Get()
        {
            var result = await _patientsService.GetAll();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetPatientByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _patientsService.GetById(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("SavePatient")]
        public async Task<IActionResult> Post([FromBody] PatientSaveDto dto)
        {
            var result = await _patientsService.SaveAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("ModifyPatient")]
        public async Task<IActionResult> Put([FromBody] PatientUpdateDto dto)
        {
            var result = await _patientsService.UpdateAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("DisablePatient")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _patientsService.DisableAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
