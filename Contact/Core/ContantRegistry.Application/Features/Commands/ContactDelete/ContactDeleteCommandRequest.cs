using ContactRegistry.Domain.Utilities;
using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactDelete;

public class ContactDeleteCommandRequest : IRequest<BaseResponse<ContactDeleteCommandResponse>>
{
    public string Id { get; set; }
}