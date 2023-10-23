using ContactRegistry.Domain.Entities;
using ContantRegistry.Application.Abstractions.Services;
using ContantRegistry.Application.Repositories;

namespace ContactRegistry.Persistence.Services;

public class ContactService : IContactService
{
    private readonly IContactReadRepository _contactReadRepository;
    private readonly IContactWriteRepository _contactWriteRepository;
    private readonly IContactFeatureReadRepository _contactFeatureReadRepository;
    private readonly IContactFeatureWriteRepository _contactFeatureWriteRepository;

    public ContactService(IContactFeatureWriteRepository contactFeatureWriteRepository, IContactFeatureReadRepository contactFeatureReadRepository, IContactWriteRepository contactWriteRepository, IContactReadRepository contactReadRepository)
    {
        _contactFeatureWriteRepository=contactFeatureWriteRepository;
        _contactFeatureReadRepository=contactFeatureReadRepository;
        _contactWriteRepository=contactWriteRepository;
        _contactReadRepository=contactReadRepository;
    }

}