using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infraestructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infraestructure.Repositories;

public class PersonRepository(MongoDBContext mongoDBContext) : IPersonRepository
{
	private readonly IMongoCollection<Person> _personCollection = mongoDBContext
		.GetConnection()
		.GetCollection<Person>(collectionName);

	private const string collectionName = "persons";

	public async Task<List<Person>> GetAll()
	{
		var allPersons = await _personCollection
			.Find(EmptyFilter())
			.ToListAsync();

		return allPersons;
	}


	public async Task<Person> InsertOne(Person person)
	{
		var generateObjectId = ObjectId.GenerateNewId();
		person._id = generateObjectId;

		await _personCollection.InsertOneAsync(person);
		return person;
	}

	public async Task<Person> UpsertOne(Person person)
	{
		var entity = await _personCollection
			.Find(x => x.name == person.name)
			.FirstOrDefaultAsync();

		if (entity == null)
		{
			await _personCollection.InsertOneAsync(person);
			return person;
		}
		else
		{
			var mergedEntity = new Person
			{
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

	public async Task<bool> DeleteOne(string objectId)
	{
		var byObjectId = FilterByObjectId(objectId);
		var resultDelete = await _personCollection.DeleteOneAsync(byObjectId);

		return resultDelete.DeletedCount > 0;
	}

	private static FilterDefinition<Person> FilterByObjectId(object objectId)
	{
		return Builders<Person>.Filter.Eq(x => x._id!, ObjectId.Parse(objectId.ToString()));
	}

	private static FilterDefinition<Person> EmptyFilter()
	{
		return Builders<Person>.Filter.Empty;
	}
}
