using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPersonRepository
{
	Task<List<Person>> GetAll();
	Task<Person> InsertOne(Person person);
	Task<Person> UpsertOne(Person person);
	Task<bool> DeleteOne(string objectId);
}
