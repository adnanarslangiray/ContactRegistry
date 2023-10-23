using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactCreate;

public class ContactCreateCommandRequest : IRequest<ContactCreateCommandResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }
}