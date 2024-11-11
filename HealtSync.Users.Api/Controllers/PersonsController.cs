using HealtSync.Domain.Entities.Users;
using HealtSync.Domain.Result;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace HealtSync.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsRepository _personsRepository;

        public PersonsController(IPersonsRepository personsRepository)
        {
            _personsRepository = personsRepository;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _personsRepository.GetAll();
            if (result.Success)
                return Ok(result.Data); // assuming OperationResult has a Data property for returning the list

            return StatusCode(500, result.Message);
        }

        // GET api/Persons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _personsRepository.GetEntityBy(id);
            if (result.Success)
                return Ok(result.Data); 

            return NotFound(result.Message);
        }

        // POST api/Persons
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Persons person)
        {
            if (person == null)
                return BadRequest("Person data is required.");

            var result = await _personsRepository.Save(person);
            if (result.Success)
                return CreatedAtAction(nameof(Get), new { id = person.PersonID }, person);
            
        
            int personId = person.PersonID;
          
            return StatusCode(500, result.Message);
        }

        // PUT api/Persons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Persons person)
        {
            if (person == null || person.PersonID != id)
                return BadRequest("Invalid data.");

            var result = await _personsRepository.Update(person);
            if (result.Success)
                return NoContent();

            return StatusCode(500, result.Message);
        }

        // DELETE api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personsRepository.GetEntityBy(id);
            if (!person.Success)
                return NotFound(person.Message);

            var result = await _personsRepository.Remove((Persons)person.Data!);
            if (result.Success)
                return NoContent();

            return StatusCode(500, result.Message);
        }
    }
}
