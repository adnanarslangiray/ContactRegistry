using AutoMapper;
using ContactRegistry.Domain.Entities;
using ContantRegistry.Application.Repositories.ContactFeature;
using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactFeatureCreate.Handler;

public class ContactFeatureCreateHandler : IRequestHandler<ContactFeatureCreateCommandRequest, ContactFeatureCreateCommandResponse>
{
    private readonly IContactFeatureWriteRepository _contactFeatureWriteRepository;
    private readonly IMapper _mapper;

    public ContactFeatureCreateHandler(IContactFeatureWriteRepository contactFeatureWriteRepository, IMapper mapper)
    {
        _contactFeatureWriteRepository=contactFeatureWriteRepository;
        _mapper=mapper;
    }

    public async Task<ContactFeatureCreateCommandResponse> Handle(ContactFeatureCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var contactFeature = _mapper.Map<ContactFeature>(request);

        var result = await _contactFeatureWriteRepository.AddAsync(contactFeature);
        if (result == true)
            await _contactFeatureWriteRepository.SaveAsync();


        return new ContactFeatureCreateCommandResponse
        {
            Success = result,
            Message = result ? "ContactFeature Created" : "Failed" ,
        };
    }
}