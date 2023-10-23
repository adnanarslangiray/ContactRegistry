using ContactRegistry.Domain.Entities.Common;

namespace ContactRegistry.Domain.Entities;

public class ContactFeature : BaseEntity
{
    public string ContactId { get; set; }
    public ContactFeatureType ContactFeatureType { get; set; }
    public string ContactFeatureValue { get; set; }
}

public enum ContactFeatureType
{
    Email,
    Phone,
    Location
}