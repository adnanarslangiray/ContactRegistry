using ContantRegistry.Application.Repositories.ContactFeature;
using MediatR;

namespace ContantRegistry.Application.Features.Commands.ContactFeatureDelete.Handler;

public class ContactFeatureDeleteHandler : IRequestHandler<ContactFeatureDeleteCommandRequest, ContactFeatureDeleteCommandResponse>
{
    private readonly IContactFeatureWriteRepository _contactFeatureWriteRepository;

    public ContactFeatureDeleteHandler(IContactFeatureWriteRepository contactFeatureWriteRepository)
    {
        _contactFeatureWriteRepository=contactFeatureWriteRepository;
    }

    public async Task<ContactFeatureDeleteCommandResponse> Handle(ContactFeatureDeleteCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _contactFeatureWriteRepository.RemoveAsync(request.Id);
        if (result == true)
            await _contactFeatureWriteRepository.SaveAsync();

        return new ContactFeatureDeleteCommandResponse
        {
            Success = result,
            Message =  result ?  "ContactFeature Deleted" : "Failed" ,
        };
    }
}