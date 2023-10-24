using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application.Repositories.ContactFeature;

namespace ContactRegistry.Persistence.Repositories.ContactFeatures;

public class ContactFeaturesWriteRepository : WriteRepository<Domain.Entities.ContactFeature>, IContactFeatureWriteRepository
{
    public ContactFeaturesWriteRepository(ContactDbContext context) : base(context)
    {
    }
}