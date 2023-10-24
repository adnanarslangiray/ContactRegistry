using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application.Repositories.ContactFeature;

namespace ContactRegistry.Persistence.Repositories.ContactFeatures;

public class ContactFeaturesReadRepository : ReadRepository<Domain.Entities.ContactFeature>, IContactFeatureReadRepository
{
    public ContactFeaturesReadRepository(ContactDbContext context) : base(context)
    {
    }
}