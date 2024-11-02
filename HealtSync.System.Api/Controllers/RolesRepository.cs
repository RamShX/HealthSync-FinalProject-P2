
using HealtSync.Persistence.Interfaces.System;
using HealtSync.Domain.Entities.System;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HealtSync.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var result = await _rolesRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _rolesRepository.GetAll();
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] Roles role)
        {
            if (role == null)
            {
                return BadRequest("El rol no puede ser nulo.");
            }

            var result = await _rolesRepository.Save(role);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetRole), new { id = role.RoleID }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Roles role)
        {
            if (id != role.RoleID)
            {
                return BadRequest("El ID del rol no coincide.");
            }

            var result = await _rolesRepository.Update(role);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var roleToRemove = new Roles { RoleID = id };

            var result = await _rolesRepository.Remove(roleToRemove);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
