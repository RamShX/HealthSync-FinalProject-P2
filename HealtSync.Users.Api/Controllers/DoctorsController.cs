using HealtSync.Application.Contracts.Users;
using HealtSync.Application.Dtos.Users.Doctors;
using Microsoft.AspNetCore.Mvc;

namespace HealtSync.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {

        private readonly IDoctorsService _doctorService;

        public DoctorsController(IDoctorsService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("GetDoctors")]
        public async Task<IActionResult> Get()
        {
            var result = await _doctorService.GetAll();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorService.GetById(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("SaveDoctor")]
        public async Task<IActionResult> Post([FromBody] DoctorSaveDto doctorSaveDto)
        {
            var result = await _doctorService.SaveAsync(doctorSaveDto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("UpdateDoctor")]
        public async Task<IActionResult> Put([FromBody] DoctorUpdateDto doctorUpdateDto)
        {
            var result = await _doctorService.UpdateAsync(doctorUpdateDto);

            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        // DELETE api/<DoctorsController>/5
        [HttpDelete("DisableDoctor")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _doctorService.DisableAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
