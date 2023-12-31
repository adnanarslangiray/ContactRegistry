﻿using ContactRegistry.Common.Utilities;
using MediatR;

namespace ContantRegistry.Application.Features.Queries.GetContacts;

public class GetContactsQueryRequest : IRequest<BasePaginationResponse<GetContactsQueryResponse>>
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}