using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class PersonDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [Required]
        [StringLength(120)]
        public string? name { get; set; }
        [Range(0, 99)]
        public int age { get; set; }
        [BsonIgnoreIfNull]
        public string? job { get; set; }
    }
}
