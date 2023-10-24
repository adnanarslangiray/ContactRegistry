using AutoMapper;
using ContactRegistry.Domain.Utilities;
using ContantRegistry.Application.Repositories.Contact;
using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactUpdate.Handlers;

public class ContactUpdateHandler : IRequestHandler<ContactUpdateCommandRequest, ContactUpdateCommandResponse>
{
    private readonly IContactWriteRepository _contactWriteRepository;
    private readonly IContactReadRepository _contactReadRepository;

    public ContactUpdateHandler(IContactWriteRepository contactWriteRepository, IContactReadRepository contactReadRepository)
    {
        _contactWriteRepository = contactWriteRepository;
        _contactReadRepository=contactReadRepository;
    }

    public async Task<ContactUpdateCommandResponse> Handle(ContactUpdateCommandRequest request, CancellationToken cancellationToken)
    {

        var result = await _contactReadRepository.GetByIdAsync(request.Id);
        result.FirstName = request.FirstName;
        result.LastName =  request.LastName;
        result.Company = request.Company;
        _contactWriteRepository.Update(result);
        var response = await _contactWriteRepository.SaveAsync();

        return new ContactUpdateCommandResponse() { Success = response == 1, Message = response == 1 ? "Contact Uptaded" : "Failed" };
    }
}