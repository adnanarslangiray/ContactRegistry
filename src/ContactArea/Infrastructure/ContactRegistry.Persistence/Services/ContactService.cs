using ContactRegistry.Domain.Entities;
using ContantRegistry.Application.Abstractions.Services;
using ContantRegistry.Application.DTOs;
using ContantRegistry.Application.Repositories.Contact;
using ContantRegistry.Application.Repositories.ContactFeature;
using Microsoft.EntityFrameworkCore;

namespace ContactRegistry.Persistence.Services;

public class ContactService : IContactService
{
    private readonly IContactReadRepository _contactReadRepository;
    private readonly IContactFeatureReadRepository _contactFeatureReadRepository;

    public ContactService(IContactReadRepository contactReadRepository, IContactFeatureReadRepository contactFeatureReadRepository)
    {
        _contactReadRepository=contactReadRepository;
        _contactFeatureReadRepository=contactFeatureReadRepository;
    }

    public async Task<ContactList> GetAllAsync(int page, int size)
    {
        var query = _contactReadRepository.Table.Include(o => o.ContactFeatures).AsNoTracking();
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

    public async Task<ContactCreateList> PrepareContactReport()
    {
        ContactCreateList contactCreatelist = new();
        var contactFeatures = _contactFeatureReadRepository.GetAll();
        var locations = contactFeatures?.Where(o => o.ContactFeatureType == ContactFeatureType.Location).ToList();

        var singleLocations = locations?
            .Select(o => o.ContactFeatureValue)
            ?.Distinct();

        foreach (var location in singleLocations)
        {
            var contacts = locations?
                .Where(o => o.ContactFeatureValue == location)?
                .Select(x => x.ContactId)?
                .Distinct();

            int? phoneCount = 0;
            try
            {
               phoneCount = contactFeatures?
                    .Where(o => o.ContactFeatureType == ContactFeatureType.Phone && contacts.Contains(o.ContactId))?
                    .Select(o => o.ContactFeatureValue)?
                    .Distinct()?
                    .Count();

            }
            catch (Exception ex)
            {
            }
            contactCreatelist.ReportDetails.Add(new ReportDetailDto
            {
                Location = location,
                PhoneNumberCount = phoneCount ?? 0,
                ContactCount = contacts.Count()
            });
        }

        return contactCreatelist;
    }
}