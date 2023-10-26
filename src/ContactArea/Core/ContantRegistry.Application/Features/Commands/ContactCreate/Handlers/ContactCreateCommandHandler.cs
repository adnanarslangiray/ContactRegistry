using AutoMapper;
using ContactRegistry.Common.Utilities;
using ContactRegistry.Domain.Entities;
using ContantRegistry.Application.Repositories.Contact;
using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactCreate.Handlers;

public class ContactCreateCommandHandler : IRequestHandler<ContactCreateCommandRequest, BaseResponse<ContactCreateCommandResponse>>
{
    private readonly IContactWriteRepository _contactWriteRepository;
    private readonly IMapper _mapper;

    public ContactCreateCommandHandler(IContactWriteRepository contactWriteRepository, IMapper mapper)
    {
        _contactWriteRepository = contactWriteRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ContactCreateCommandResponse>> Handle(ContactCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var contact = _mapper.Map<Contact>(request);
        if (contact == null)
            throw new ApplicationException("Contact could not mapped!");
        var result = await _contactWriteRepository.AddAsync(contact);
        await _contactWriteRepository.SaveAsync();


        return new BaseResponse<ContactCreateCommandResponse>()
        {
            Data =result ? _mapper.Map<ContactCreateCommandResponse>(contact) : default,
            Message =result ? "Contact Added" : "Contact could not added!",
            Success = result
        };
;
    }
}