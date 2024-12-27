using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class PersonService(
	IPersonRepository personRepository,
	IMapper mapper) : IPersonService
{
	public async Task<List<PersonDto>> GetAll()
	{
		var entities = await personRepository.GetAll();

		return mapper.Map<List<PersonDto>>(entities);
	}

	public async Task<PersonDto?> GetById(Guid id)
	{
		var entity = await personRepository.GetById(id);

		if (entity is null) return null;

		return mapper.Map<PersonDto>(entity);
	}

	public async Task<PersonDto> InsertOne(PersonDto personDto)
	{
		var personExist = await GetById(personDto.Id);

		if (personExist is not null)
			throw new EntityAlreadyExistException("Entity already exist in database");

		var person = mapper.Map<Person>(personDto);

		var result = await personRepository.InsertOne(person);

		return mapper.Map<PersonDto>(result);
	}

	public async Task<PersonDto> UpdateOne(PersonDto personDto)
	{
		var personExist = await GetById(personDto.Id);

		if (personExist is null)
			throw new EntityNotFoundException("Entity doesn't exist in database");

		var person = mapper.Map<Person>(personDto);

		var result = await personRepository.UpdateOne(person);

		return mapper.Map<PersonDto>(result);
	}

	public async Task<bool> DeleteOne(Guid id)
	{
		var result = await personRepository.DeleteOne(id);

		return result;
	}
}
