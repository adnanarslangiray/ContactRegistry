using ContactRegistry.Domain.Utilities;
using ContantRegistry.Application.Repositories;
using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactDelete.Handlers;

public class ContactDeleteHandler : IRequestHandler<ContactDeleteCommandRequest, BaseResponse<ContactDeleteCommandResponse>>
{
    private readonly IContactWriteRepository _contactWriteRepository;

    public ContactDeleteHandler(IContactWriteRepository contactWriteRepository)
    {
        _contactWriteRepository = contactWriteRepository;
    }

    public async Task<BaseResponse<ContactDeleteCommandResponse>> Handle(ContactDeleteCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _contactWriteRepository.RemoveAsync(request.Id);
        await _contactWriteRepository.SaveAsync();

        return new BaseResponse<ContactDeleteCommandResponse>(new(), result);
    }
}