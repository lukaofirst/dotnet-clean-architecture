namespace Infraestructure.Settings;

public class MongoDBSettings
{
	public const string BindName = "MongoDB";

	public string? MONGO_URI { get; set; }
	public string? DATABASE_NAME { get; set; }
}
