using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController(IPersonService personService) : ControllerBase
{

	[HttpGet(Name = "GetAll")]
	public async Task<IActionResult> GetAll()
	{
		var persons = await personService.GetAll();

		return Ok(persons);
	}

	[HttpPost]
	public async Task<IActionResult> Post(PersonDTO person)
	{
		if (person == null) return BadRequest();

		var entity = await personService.InsertOne(person);

		return CreatedAtRoute("GetAll", entity);
	}

	[HttpPut]
	public async Task<IActionResult> Upsert(PersonDTO person)
	{
		var resultEntity = await personService.UpsertOne(person);

		return Ok(resultEntity);
	}

	[HttpDelete("{objectId}")]
	public async Task<IActionResult> Delete(string objectId)
	{
		var result = await personService.DeleteOne(objectId);

		return result ?
			Ok(new { message = "Person deleted successfully!" }) : NotFound();
	}
}
