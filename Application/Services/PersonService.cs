using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class PersonService(
	IPersonRepository personRepository,
	IMapper mapper) : IPersonService
{
	public async Task<List<PersonDTO>> GetAll()
	{
		var entities = await personRepository.GetAll();

		return mapper.Map<List<PersonDTO>>(entities);
	}

	public async Task<PersonDTO> InsertOne(PersonDTO person)
	{
		var mappedEntity = mapper.Map<Person>(person);
		var resultEntity = await personRepository.InsertOne(mappedEntity);

		return mapper.Map<PersonDTO>(resultEntity);
	}

	public async Task<PersonDTO> UpsertOne(PersonDTO person)
	{
		var mappedEntity = mapper.Map<Person>(person);
		var resultEntity = await personRepository.UpsertOne(mappedEntity);

		return mapper.Map<PersonDTO>(resultEntity);
	}

	public async Task<bool> DeleteOne(string objectId)
	{
		var result = await personRepository.DeleteOne(objectId);

		return result;
	}
}
