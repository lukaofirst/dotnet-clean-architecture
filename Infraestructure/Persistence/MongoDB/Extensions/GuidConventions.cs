using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Infraestructure.Persistence.MongoDB.Extensions;

public class GuidConvention : IMemberMapConvention
{
	public string Name => nameof(GuidConvention);

	public void Apply(BsonMemberMap memberMap)
	{
		if (memberMap.MemberType == typeof(Guid))
		{
			memberMap.SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
		}
	}
}