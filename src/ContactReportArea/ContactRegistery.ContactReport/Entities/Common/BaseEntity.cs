using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContactRegistery.ContactReport.Entities.Common;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [BsonRepresentation(BsonType.DateTime)]
    public virtual DateTime UpdatedDate { get; set; }
}