using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IPersonService
{
	Task<List<PersonDTO>> GetAll();
	Task<PersonDTO> InsertOne(PersonDTO person);
	Task<PersonDTO> UpsertOne(PersonDTO person);
	Task<bool> DeleteOne(string objectId);
}
