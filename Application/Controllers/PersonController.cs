using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personRepository.GetAll();

            return Ok(persons);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            if (person == null) return BadRequest();

            if (String.IsNullOrWhiteSpace(person.name) || person.age <= 0)
                return ValidationProblem("Name cannot be empty and Age cannot be 0 or negative");

            var entity = await _personRepository.InsertOne(person);

            return CreatedAtRoute("GetAll", entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Person person)
        {
            if (String.IsNullOrWhiteSpace(person.name) || person.age <= 0)
                return ValidationProblem("Name cannot be empty and Age cannot be 0 or negative");

            await _personRepository.UpdateOne(person);

            return Ok(person);
        }

        [HttpDelete("{objectId}")]
        public async Task<IActionResult> Delete(string objectId)
        {
            await _personRepository.DeleteOne(objectId);

            return Ok(new { message = "Person deleted successfully!" });
        }
    }
}
