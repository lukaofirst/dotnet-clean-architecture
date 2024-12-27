using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController(IPersonService personService) : ControllerBase
{
	[HttpGet(Name = nameof(GetAll))]
	public async Task<IActionResult> GetAll()
	{
		var persons = await personService.GetAll();

		return Ok(persons);
	}

	[HttpGet("{id:guid}", Name = nameof(GetById))]
	public async Task<IActionResult> GetById(Guid id)
	{
		var person = await personService.GetById(id);

		return person is not null ? Ok(person) : NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> Post(PersonDto person)
	{
		try
		{
			var entity = await personService.InsertOne(person);

			return CreatedAtRoute(nameof(GetById), new { id = entity.Id }, entity);
		}
		catch (Exception ex) when (ex is EntityAlreadyExistException)
		{
			return Conflict(ex.Message);
		}
	}

	[HttpPut]
	public async Task<IActionResult> Update(PersonDto person)
	{
		try
		{
			var result = await personService.UpdateOne(person);

			return Ok(result);
		}
		catch (Exception ex) when (ex is EntityNotFoundException)
		{
			return NotFound(ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await personService.DeleteOne(id);

		return result ?
			Ok(new { message = "Person deleted successfully!" }) : NotFound();
	}
}
