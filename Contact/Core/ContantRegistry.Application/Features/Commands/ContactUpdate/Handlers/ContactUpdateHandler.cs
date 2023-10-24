using AutoMapper;
using ContactRegistry.Domain.Utilities;
using ContantRegistry.Application.Repositories;
using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactUpdate.Handlers;

public class ContactUpdateHandler : IRequestHandler<ContactUpdateCommandRequest, BaseResponse<ContactUpdateCommandResponse>>
{
    private readonly IContactWriteRepository _contactWriteRepository;
    private readonly IContactReadRepository _contactReadRepository;

    public ContactUpdateHandler(IContactWriteRepository contactWriteRepository, IContactReadRepository contactReadRepository)
    {
        _contactWriteRepository = contactWriteRepository;
        _contactReadRepository=contactReadRepository;
    }

    public async Task<BaseResponse<ContactUpdateCommandResponse>> Handle(ContactUpdateCommandRequest request, CancellationToken cancellationToken)
    {

        var result = await _contactReadRepository.GetByIdAsync(request.Id);
        request.FirstName = result.FirstName;
        request.LastName = result.LastName;
        request.Company = result.Company;
        var response = await _contactWriteRepository.SaveAsync();

        return new BaseResponse<ContactUpdateCommandResponse>(new(), response == 1);
    }
}