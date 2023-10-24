using MediatR;

namespace ContantRegistry.Application.Features.Queries.GetContacts;

public class GetContactsQueryRequest : IRequest<GetContactsQueryResponse>
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
}