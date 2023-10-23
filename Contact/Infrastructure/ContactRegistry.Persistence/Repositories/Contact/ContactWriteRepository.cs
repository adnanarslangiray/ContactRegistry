using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application.Repositories;

namespace ContactRegistry.Persistence.Repositories;

public class ContactWriteRepository : WriteRepository<ContactRegistry.Domain.Entities.Contact>, IContactWriteRepository
{
    public ContactWriteRepository(ContactDbContext context) : base(context)
    {
    }
}