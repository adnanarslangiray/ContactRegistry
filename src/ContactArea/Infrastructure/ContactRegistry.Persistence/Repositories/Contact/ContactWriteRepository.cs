using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application.Repositories.Contact;

namespace ContactRegistry.Persistence.Repositories.Contact;

public class ContactWriteRepository : WriteRepository<Domain.Entities.Contact>, IContactWriteRepository
{
    public ContactWriteRepository(ContactDbContext context) : base(context)
    {
    }
}