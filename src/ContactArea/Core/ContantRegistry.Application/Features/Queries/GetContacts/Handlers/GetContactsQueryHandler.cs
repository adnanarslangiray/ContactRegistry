using ContactRegistry.Common.Utilities;
using ContantRegistry.Application.Abstractions.Services;
using MediatR;

namespace ContantRegistry.Application.Features.Queries.GetContacts.Handlers;

public class GetContactsQueryHandler : IRequestHandler<GetContactsQueryRequest, BasePaginationResponse<GetContactsQueryResponse>>
{
    private readonly IContactService _contactService;

    public GetContactsQueryHandler(IContactService contactService)
    {
        _contactService=contactService;
    }

    public async Task<BasePaginationResponse<GetContactsQueryResponse>> Handle(GetContactsQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _contactService.GetAllAsync(request.Page, request.Size);

        var data = new GetContactsQueryResponse() { Contacts = result.Contacts };
        return new BasePaginationResponse<GetContactsQueryResponse>(
            data: data,
            success: true,
            message: "Contacts listed successfully.",
            totalCount: result.TotalCount,
            currentPageIndex: request.Page,
            pageSize: request.Size);
    }
}