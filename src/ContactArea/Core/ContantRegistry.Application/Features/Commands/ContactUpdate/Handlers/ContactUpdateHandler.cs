using ContactRegistry.Domain.Entities;
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
        _= await _contactWriteRepository.SaveAsync();
        if (result == null)
             return new ContactUpdateCommandResponse() { Success = false, Message = "Contact not found" };
        result.FirstName = request.FirstName;
        result.LastName =  request.LastName;
        result.Company = request.Company;


        _contactWriteRepository.Update(result);

        _= await _contactWriteRepository.SaveAsync();

        return new ContactUpdateCommandResponse() { Success = true, Message = "Contact Uptaded" };
    }
}