using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPersonRepository
{
	Task<List<Person>> GetAll();
	Task<Person?> GetById(Guid id);
	Task<Person> InsertOne(Person person);
	Task<Person> UpdateOne(Person person);
	Task<bool> DeleteOne(Guid id);
}
