using ContantRegistry.Application.Abstractions.Services;
using MediatR;

namespace ContantRegistry.Application.Features.Queries.GetContacts.Handlers;

public class GetContactsQueryHandler : IRequestHandler<GetContactsQueryRequest, GetContactsQueryResponse>
{
    private readonly IContactService _contactService;

    public GetContactsQueryHandler(IContactService contactService)
    {
        _contactService=contactService;
    }

    public async Task<GetContactsQueryResponse> Handle(GetContactsQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _contactService.GetAllAsync(request.Page, request.Size);
        return new GetContactsQueryResponse()
        {

            Contacts = result.Contacts,
            TotalCount = result.TotalCount,
        };
    }
}