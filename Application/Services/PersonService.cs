using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public PersonService(IMapper mapper, IPersonRepository personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
        }

        public async Task<List<PersonDTO>> GetAll()
        {
            var entities = await _personRepository.GetAll();

            return _mapper.Map<List<PersonDTO>>(entities);
        }
        
        public async Task<PersonDTO> InsertOne(PersonDTO person)
        {
            var mappedEntity = _mapper.Map<Person>(person);
            var resultEntity = await _personRepository.InsertOne(mappedEntity);

            return _mapper.Map<PersonDTO>(resultEntity);
        }

        public async Task<PersonDTO> UpsertOne(PersonDTO person)
        {
            var mappedEntity = _mapper.Map<Person>(person);
            var resultEntity = await _personRepository.UpsertOne(mappedEntity);

            return _mapper.Map<PersonDTO>(resultEntity);
        }

        public async Task DeleteOne(string objectId) => await _personRepository.DeleteOne(objectId);
    }
}
