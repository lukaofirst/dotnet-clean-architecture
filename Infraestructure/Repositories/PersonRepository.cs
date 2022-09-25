using Core.Entities;
using Core.Interfaces.Repositories;
using Infraestructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infraestructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MongoDBContext _mongoDBContext;
        private readonly IMongoCollection<Person> _personCollection;
        private const string collectionName = "persons";

        public PersonRepository(MongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
            _personCollection = _mongoDBContext.GetConn().GetCollection<Person>(collectionName);
        }

        public async Task<List<Person>> GetAll()
        {
            var allPersons = await _personCollection.Find(EmptyFilter()).ToListAsync();

            return allPersons;
        }


        public async Task<Person> InsertOne(Person person)
        {
            await _personCollection.InsertOneAsync(person);
            return person;
        }

        public async Task<Person> UpsertOne(Person person)
        {
            var entity = await _personCollection
                .Find(x => x.name == person.name).FirstOrDefaultAsync();

            if (entity == null)
            {
                await _personCollection.InsertOneAsync(person);
                return person;
            }
            else
            {
                var mergedEntity = new Person { 
                    _id = entity._id, 
                    name = person.name, 
                    age = person.age, 
                    job = person.job 
                };

                var options = new ReplaceOptions { IsUpsert = true };
                var resultUpdatedEntity = await _personCollection
                    .ReplaceOneAsync(FilterByObjectId(entity._id!), mergedEntity, options);

                return mergedEntity;
            }
        }

        public async Task DeleteOne(string objectId)
        {
            FilterDefinition<Person> byObjectId = FilterByObjectId(objectId);
            await _personCollection.DeleteOneAsync(byObjectId);
        }

        private static FilterDefinition<Person> FilterByObjectId(string objectId)
        {
            return Builders<Person>.Filter.Eq(x => x._id!, ObjectId.Parse(objectId).ToString());
        }

        private static FilterDefinition<Person> EmptyFilter()
        {
            return Builders<Person>.Filter.Empty;
        }
    }
}
