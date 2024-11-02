using HealtSync.Domain.Entities.System;
using HealtSync.Persistence.Interfaces.System;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace HealtSync.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus(int id)
        {
            var result = await _statusRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            var result = await _statusRepository.GetAll();
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] Status status)
        {
            if (status == null)
            {
                return BadRequest("El status no puede ser nulo.");
            }

            var result = await _statusRepository.Save(status);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetStatus), new { id = status.StatusID }, status);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] Status status)
        {
            if (id != status.StatusID)
            {
                return BadRequest("El ID del status no coincide.");
            }

            var result = await _statusRepository.Update(status);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var statusToRemove = new Status { StatusID = id };

            var result = await _statusRepository.Remove(statusToRemove);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
