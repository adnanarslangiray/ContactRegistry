using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application.Repositories;

namespace ContactRegistry.Persistence.Repositories;

public class ContactFeaturesWriteRepository : WriteRepository<ContactRegistry.Domain.Entities.ContactFeature>, IContactFeatureWriteRepository
{
    public ContactFeaturesWriteRepository(ContactDbContext context) : base(context)
    {
    }
}
