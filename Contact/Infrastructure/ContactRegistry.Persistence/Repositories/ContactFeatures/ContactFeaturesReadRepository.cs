using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application.Repositories;

namespace ContactRegistry.Persistence.Repositories;

public class ContactFeaturesReadRepository : ReadRepository<ContactRegistry.Domain.Entities.ContactFeature>, IContactFeatureReadRepository
{
    public ContactFeaturesReadRepository(ContactDbContext context) : base(context)
    {
    }
}