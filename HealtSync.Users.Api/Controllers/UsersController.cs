
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace HealtSync.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
       
        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get()
        {
            var result = await  _usersRepository.GetAll();

            if(!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("SaveUser")]
        public async Task<IActionResult> Post([FromBody] Domain.Entities.Users.Users user)
        {
            var result = await _usersRepository.Save(user);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }

        [HttpPut("ModifyUser")]
        public async Task<IActionResult> Put([FromBody]Domain.Entities.Users.Users user)
        {
            var result = await _usersRepository.Update(user);

            if (!result.Success)
                return BadRequest(result);

            return Ok();
        }

        [HttpDelete("DisableUser")]
        public async Task<IActionResult> DisableUser([FromBody] Domain.Entities.Users.Users user)
        {
            var result = await _usersRepository.Remove(user);

            if (!result.Success)
                return BadRequest(result);

            return Ok();
        }
    }
}
