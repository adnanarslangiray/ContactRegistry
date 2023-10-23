using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application.Repositories;

namespace ContactRegistry.Persistence.Repositories;

public class ContactReadRepository : ReadRepository<ContactRegistry.Domain.Entities.Contact>, IContactReadRepository
{
    public ContactReadRepository(ContactDbContext context) : base(context)
    {
    }
}