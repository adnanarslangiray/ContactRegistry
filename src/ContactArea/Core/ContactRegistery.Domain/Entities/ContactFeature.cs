using ContactRegistry.Domain.Entities.Common;

namespace ContactRegistry.Domain.Entities;

public class ContactFeature : BaseEntity
{
    public Guid ContactId { get; set; }
    public ContactFeatureType ContactFeatureType { get; set; }
    public string ContactFeatureValue { get; set; }
}

public enum ContactFeatureType
{
    Email = 1,
    Phone = 2,
    Location = 3
}