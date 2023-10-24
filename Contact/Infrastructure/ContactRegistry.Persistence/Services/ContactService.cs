using ContactRegistry.Domain.Entities;
using ContantRegistry.Application.Abstractions.Services;
using ContantRegistry.Application.DTOs;
using ContantRegistry.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContactRegistry.Persistence.Services;

public class ContactService : IContactService
{
    private readonly IContactReadRepository _contactReadRepository;

    public ContactService(IContactReadRepository contactReadRepository)
    {
        _contactReadRepository=contactReadRepository;
    }

    public async Task<ContactList> GetAllAsync(int page, int size)
    {
        var query = _contactReadRepository.Table.Include(o => o.ContactFeatures);
        var list = query.Skip(page * size).Take(size);
        var count = await query.CountAsync();
        return new ContactList
        {
            Contacts = list,
            TotalCount = count
        };
    }

    public async Task<Contact> GetbyIdAsync(string id)
    {
        var query = _contactReadRepository.Table.Include(o => o.ContactFeatures);
        return await query.FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
    }
}