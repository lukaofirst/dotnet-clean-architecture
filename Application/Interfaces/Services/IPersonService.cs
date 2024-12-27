using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IPersonService
{
	Task<List<PersonDto>> GetAll();
	Task<PersonDto?> GetById(Guid id);
	Task<PersonDto> InsertOne(PersonDto personDto);
	Task<PersonDto> UpdateOne(PersonDto personDto);
	Task<bool> DeleteOne(Guid id);
}
