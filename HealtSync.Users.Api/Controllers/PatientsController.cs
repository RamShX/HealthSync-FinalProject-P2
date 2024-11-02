using HealtSync.Persistence.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HealtSync.Domain.Entities.Users;

namespace HealtSync.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;

        public PatientsController(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }

        [HttpGet("GetPatients")]
        public async Task<IActionResult> Get()
        {
            var result = await _patientsRepository.GetAll();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("SavePatient")]
        public async Task<IActionResult> Post([FromBody] Patients patient)
        {
            var result = await _patientsRepository.Save(patient);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("ModifyPatient")]
        public async Task<IActionResult> Put([FromBody] Patients patient)
        {
            var result = await _patientsRepository.Update(patient);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("DisablePatient")]
        public async Task<IActionResult> Delete([FromBody] Patients patient)
        {
            var result = await _patientsRepository.Remove(patient);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
