using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personService.GetAll();

            return Ok(persons);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            if (person == null) return BadRequest();

            if (String.IsNullOrWhiteSpace(person.name) || person.age <= 0)
                return ValidationProblem("Name cannot be empty and Age cannot be 0 or negative");

            var entity = await _personService.InsertOne(person);

            return CreatedAtRoute("GetAll", entity);
        }

        [HttpPut]
        public async Task<IActionResult> Upsert(Person person)
        {
            if (String.IsNullOrWhiteSpace(person.name) || person.age <= 0)
                return ValidationProblem("Name cannot be empty and Age cannot be 0 or negative");

            await _personService.UpsertOne(person);

            return Ok(person);
        }

        [HttpDelete("{objectId}")]
        public async Task<IActionResult> Delete(string objectId)
        {
            await _personService.DeleteOne(objectId);

            return Ok(new { message = "Person deleted successfully!" });
        }
    }
}
