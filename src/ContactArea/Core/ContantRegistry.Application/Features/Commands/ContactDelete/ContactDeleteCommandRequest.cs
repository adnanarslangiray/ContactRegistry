using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactDelete;

public class ContactDeleteCommandRequest : IRequest<ContactDeleteCommandResponse>
{
    public string Id { get; set; }
}