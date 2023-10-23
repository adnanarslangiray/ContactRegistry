using AutoMapper;
using ContactRegistry.Domain.Entities;
using ContactRegistry.Domain.Utilities;
using ContantRegistry.Application.Features.Commands.ContactCreate;
using ContantRegistry.Application.Repositories;
using MediatR;

namespace ContantRegistry.Application.Features.Handlers;

public class ContactCreateHandler : IRequestHandler<ContactCreateCommandRequest, BaseResponse<ContactCreateCommandResponse>>
{
    private readonly IContactWriteRepository _contactWriteRepository;
    private readonly IMapper _mapper;

    public ContactCreateHandler(IContactWriteRepository contactWriteRepository, IMapper mapper)
    {
        _contactWriteRepository=contactWriteRepository;
        _mapper=mapper;
    }

    public async Task<BaseResponse<ContactCreateCommandResponse>> Handle(ContactCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var contact = _mapper.Map<Contact>(request);
        if (contact == null)
            throw new ApplicationException("Contact could not mapped!");
        var result = await _contactWriteRepository.AddAsync(contact);
        if (result == null)
            


        return new();
    }
}