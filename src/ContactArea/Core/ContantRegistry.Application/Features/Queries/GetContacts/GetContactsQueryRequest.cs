using ContactRegistry.Domain.Utilities;
using MediatR;

namespace ContantRegistry.Application.Features.Queries.GetContacts;

public class GetContactsQueryRequest : IRequest<BasePaginationResponse<GetContactsQueryResponse>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}