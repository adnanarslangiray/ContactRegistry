using ContactRegistry.Domain.Utilities;
using MediatR;

namespace ContantRegistry.Application.Features.Queries.GetContactById;

public class GetContactByIdQueryRequest : IRequest<BaseResponse<GetContactByIdQueryResponse>>
{
    public string Id { get; set; }
}