using AutoMapper;
using ContactRegistry.Domain.Utilities;
using ContantRegistry.Application.Abstractions.Services;
using MediatR;

namespace ContantRegistry.Application.Features.Queries.GetContactById.Handlers;

public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQueryRequest, BaseResponse<GetContactByIdQueryResponse>>
{
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;

    public GetContactByIdQueryHandler(IContactService contactService, IMapper mapper)
    {
        _contactService=contactService;
        _mapper=mapper;
    }

    public async Task<BaseResponse<GetContactByIdQueryResponse>> Handle(GetContactByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _contactService.GetbyIdAsync(request.Id);
      
        var contact = _mapper.Map<GetContactByIdQueryResponse>(result);

        return new BaseResponse<GetContactByIdQueryResponse>() { Data = contact, Success = result != null};
    }
}