using ContactRegistry.ContactReport.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContactRegistry.ContactReport.Entities;

public class ReportDetail : BaseEntity
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string ReportId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string Location { get; set; }

    [BsonRepresentation(BsonType.Int32)]
    public int ContactCount { get; set; }

    [BsonRepresentation(BsonType.Int32)]
    public int PhoneNumberCount { get; set; }
}