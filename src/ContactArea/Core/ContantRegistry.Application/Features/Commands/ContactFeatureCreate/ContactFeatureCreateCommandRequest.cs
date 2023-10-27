using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactFeatureCreate;

public class ContactFeatureCreateCommandRequest : IRequest<ContactFeatureCreateCommandResponse>
{
    public string ContactId { get; set; }
    public int ContactFeatureType { get; set; }
    public string ContactFeatureValue { get; set; }
}