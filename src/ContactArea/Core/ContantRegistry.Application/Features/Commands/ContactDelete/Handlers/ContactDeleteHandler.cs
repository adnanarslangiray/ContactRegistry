using ContantRegistry.Application.Repositories.Contact;
using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactDelete.Handlers;

public class ContactDeleteHandler : IRequestHandler<ContactDeleteCommandRequest, ContactDeleteCommandResponse>
{
    private readonly IContactWriteRepository _contactWriteRepository;

    public ContactDeleteHandler(IContactWriteRepository contactWriteRepository)
    {
        _contactWriteRepository = contactWriteRepository;
    }

    public async Task<ContactDeleteCommandResponse> Handle(ContactDeleteCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _contactWriteRepository.RemoveAsync(request.Id);
        await _contactWriteRepository.SaveAsync();
        return new ContactDeleteCommandResponse() { Success = result, Message = result ? "Contact Deleted" : "Failed" };
    }
}