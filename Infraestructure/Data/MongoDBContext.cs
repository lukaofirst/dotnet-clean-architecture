using Infraestructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infraestructure.Data;

public class MongoDBContext
{
	private readonly IMongoDatabase db;

	public MongoDBContext(IOptions<MongoDBSettings> mongoDBSettingsOptions)
	{
		var client = new MongoClient(mongoDBSettingsOptions.Value.MONGO_URI);
		db = client.GetDatabase(mongoDBSettingsOptions.Value.DATABASE_NAME);
	}

	public IMongoDatabase GetConn()
	{
		return db;
	}
}
