using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactUpdate;

public class ContactUpdateCommandRequest : IRequest<ContactUpdateCommandResponse>
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }
}