using AutoMapper;
using ContactRegistry.Domain.Entities;
using ContantRegistry.Application.Repositories;
using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactCreate.Handlers;

public class ContactCreateCommandHandler : IRequestHandler<ContactCreateCommandRequest, ContactCreateCommandResponse>
{
    private readonly IContactWriteRepository _contactWriteRepository;
    private readonly IMapper _mapper;

    public ContactCreateCommandHandler(IContactWriteRepository contactWriteRepository, IMapper mapper)
    {
        _contactWriteRepository = contactWriteRepository;
        _mapper = mapper;
    }

    public async Task<ContactCreateCommandResponse> Handle(ContactCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var contact = _mapper.Map<Contact>(request);
        if (contact == null)
            throw new ApplicationException("Contact could not mapped!");
        var result = await _contactWriteRepository.AddAsync(contact);
        await _contactWriteRepository.SaveAsync();
        if (result == null)
            throw new ApplicationException("Contact could not added!");

        var response = new ContactCreateCommandResponse() { Success = result, Message = result ? "Contact Added" : "Failed" };
        return response;
    }
}