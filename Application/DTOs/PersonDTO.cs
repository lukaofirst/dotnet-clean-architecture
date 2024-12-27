using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class PersonDTO
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string? Id { get; set; }

	[BsonElement("name")]
	[Required]
	[StringLength(120)]
	public string? Name { get; set; }

	[BsonElement("age")]
	[Range(0, 99)]
	public int Age { get; set; }

	[BsonElement("job")]
	[BsonIgnoreIfNull]
	public string? Job { get; set; }
}
