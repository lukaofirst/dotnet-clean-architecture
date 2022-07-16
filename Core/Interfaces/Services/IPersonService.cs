using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IPersonService
    {
        Task<List<Person>> GetAll();
        Task<Person> InsertOne(Person person);
        Task<Person> UpsertOne(Person person);
        Task DeleteOne(string objectId);
    }
}
