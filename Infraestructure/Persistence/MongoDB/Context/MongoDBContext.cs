using Infraestructure.Persistence.MongoDB.Extensions;
using Infraestructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Infraestructure.Persistence.MongoDB.Context;

public class MongoDBContext
{
	private readonly IMongoDatabase db;

	public MongoDBContext(IOptions<MongoDBSettings> mongoDBSettingsOptions)
	{
		var client = new MongoClient(mongoDBSettingsOptions.Value.MONGO_URI);

		var conventionPack = new ConventionPack
		{
			new GuidConvention()
		};

		ConventionRegistry.Register(nameof(GuidConvention.Name), conventionPack, t => true);  // Apply to all types

		db = client.GetDatabase(mongoDBSettingsOptions.Value.DATABASE_NAME);
	}

	public IMongoDatabase GetConnection()
	{
		return db;
	}
}
