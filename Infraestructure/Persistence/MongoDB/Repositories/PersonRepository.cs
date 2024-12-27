using Domain.Interfaces.Repositories;
using Domain.Entities;
using MongoDB.Driver;
using Infraestructure.Persistence.MongoDB.Context;

namespace Infraestructure.Persistence.MongoDB.Repositories;

public class PersonRepository(MongoDBContext mongoDBContext) : IPersonRepository
{
	private const string collectionName = "persons";

	private readonly IMongoCollection<Person> _personCollection = mongoDBContext
		.GetConnection()
		.GetCollection<Person>(collectionName);

	public async Task<List<Person>> GetAll()
	{
		var entities = await _personCollection
			.Find(EmptyFilter())
			.ToListAsync();

		return entities;
	}

	public async Task<Person?> GetById(Guid id)
	{
		var entity = await _personCollection
			.Find(x => x.Id == id)
			.FirstOrDefaultAsync();

		return entity;
	}

	public async Task<Person> InsertOne(Person person)
	{
		person.Id = Guid.CreateVersion7();

		await _personCollection.InsertOneAsync(person);

		return person;
	}

	public async Task<Person> UpdateOne(Person person)
	{
		var options = new ReplaceOptions { IsUpsert = false };

		await _personCollection.ReplaceOneAsync(FilterByObjectId(person.Id!), person, options);

		return person;
	}

	public async Task<bool> DeleteOne(Guid id)
	{
		var personFilterDefinition = FilterByObjectId(id);

		var resultDelete = await _personCollection.DeleteOneAsync(personFilterDefinition);

		return resultDelete.DeletedCount > 0;
	}

	private static FilterDefinition<Person> FilterByObjectId(Guid id)
	{
		return Builders<Person>.Filter.Eq(x => x.Id, id);
	}

	private static FilterDefinition<Person> EmptyFilter()
	{
		return Builders<Person>.Filter.Empty;
	}
}
