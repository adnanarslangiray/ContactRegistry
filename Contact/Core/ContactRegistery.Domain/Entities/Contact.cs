using ContactRegistry.Domain.Entities.Common;

namespace ContactRegistry.Domain.Entities;

public class Contact : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }
    public ICollection<ContactFeature> ContactFeatures { get; set; }
}