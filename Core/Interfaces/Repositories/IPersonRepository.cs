using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAll(); 
        Task<Person> InsertOne(Person person);
        Task<Person> UpdateOne(Person person);
        Task DeleteOne(string objectId);
    }
}
