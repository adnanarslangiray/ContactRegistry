using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactFeatureDelete;

public class ContactFeatureDeleteCommandRequest : IRequest<ContactFeatureDeleteCommandResponse>
{
    public string Id { get; set; }
}