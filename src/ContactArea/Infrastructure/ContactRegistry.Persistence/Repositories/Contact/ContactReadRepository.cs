using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application.Repositories.Contact;

namespace ContactRegistry.Persistence.Repositories.Contact;

public class ContactReadRepository : ReadRepository<Domain.Entities.Contact>, IContactReadRepository
{
    public ContactReadRepository(ContactDbContext context) : base(context)
    {
    }
}