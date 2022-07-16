using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infraestructure.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<List<Person>> GetAll() => await _personRepository.GetAll();
        
        public async Task<Person> InsertOne(Person person) => await _personRepository.InsertOne(person);

        public async Task<Person> UpsertOne(Person person) => await _personRepository.UpsertOne(person);

        public async Task DeleteOne(string objectId) => await _personRepository.DeleteOne(objectId);
    }
}
