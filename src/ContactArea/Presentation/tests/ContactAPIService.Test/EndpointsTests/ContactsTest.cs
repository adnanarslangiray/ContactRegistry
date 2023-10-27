using ContactRegistry.Common.Utilities;
using ContactRegistry.ContactAPI.Controllers.v1;
using ContantRegistry.Application.Features.Commands.ContactCreate;
using ContantRegistry.Application.Features.Commands.ContactDelete;
using ContantRegistry.Application.Features.Commands.ContactFeatureCreate;
using ContantRegistry.Application.Features.Commands.ContactFeatureDelete;
using ContantRegistry.Application.Features.Commands.ContactUpdate;
using ContantRegistry.Application.Features.Queries.GetContactById;
using ContantRegistry.Application.Features.Queries.GetContacts;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ContactAPIService.Test.EndpointsTests;

public class ContactsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly ContactsController _contactsController;
    private readonly ContactFeaturesController _contactFeaturesController;
    private const string ContactId1 = "3ca0ee4a-e954-4603-8c60-7205dc7167e6";
    private const string ContactId2 = "e0addc71-ab3b-4352-8a65-dcecfeef231b";

    public ContactsTest()
    {
        _mediator = new Mock<IMediator>();
        _contactsController = new ContactsController(_mediator.Object);
        _contactFeaturesController = new ContactFeaturesController(_mediator.Object);
    }

    [Fact]
    public async Task GetContacts_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        _mediator.Setup(_ => _.Send(It.IsAny<GetContactsQueryRequest>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(GetMockContactList());

        // Act
        var result = await _contactsController.GetContacts(new GetContactsQueryRequest());
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetContactById_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        _mediator.Setup(_ => _.Send(It.IsAny<GetContactByIdQueryRequest>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(GetMockContactById(ContactId1));

        // Act
        var result = await _contactsController.GetContactById(new GetContactByIdQueryRequest());
        // Assert
        var objectResult = (ObjectResult)result;
        var res = (BaseResponse<GetContactByIdQueryResponse>)objectResult.Value;
        res.Data.Id.Should().Be(Guid.Parse(ContactId1));
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task CreateContact_WhenCalled_ReturnsOkResult()
    {
        var contact = GetMockContactById(ContactId1);
        // Arrange
        _mediator.Setup(_ => _.Send(It.IsAny<ContactCreateCommandRequest>(), It.IsAny<CancellationToken>()))
          .Returns(Task.FromResult(MockContactCreate()));
        // Act
        var result = await _contactsController.CreateContact(new ContactCreateCommandRequest());
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objResult = (OkObjectResult)result;
        var contactResult = (BaseResponse<ContactCreateCommandResponse>)objResult.Value;
        Assert.NotNull(contactResult);
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    //delete contact
    [Fact]
    public async Task DeleteContact_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        _mediator.Setup(_ => _.Send(It.IsAny<ContactDeleteCommandRequest>(), It.IsAny<CancellationToken>()))
          .Returns(Task.FromResult(DeleteMockResponse()));
        // Act
        var result = await _contactsController.DeleteContact(new ContactDeleteCommandRequest());
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objResult = (OkObjectResult)result;
        var contactResult = (ContactDeleteCommandResponse)objResult.Value;
         Assert.True(contactResult.Success);
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }
    [Fact]
    public async Task UpdateContact_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        _mediator.Setup(_ => _.Send(It.IsAny<ContactUpdateCommandRequest>(), It.IsAny<CancellationToken>()))
          .Returns(Task.FromResult(UpdateMockResponse()));
        // Act
        var result = await _contactsController.UpdateContact(new ContactUpdateCommandRequest());
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objResult = (OkObjectResult)result;
        var contactResult = (ContactUpdateCommandResponse)objResult.Value;
        Assert.True(contactResult.Success);
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    #region ContactFeaturesController
    [Fact]
     //create
     public async Task CreateContactFeature_WhenCalled_ReturnsOkResult()
    {

        // Arrange
        _mediator.Setup(_ => _.Send(It.IsAny<ContactFeatureCreateCommandRequest>(), It.IsAny<CancellationToken>()))
          .Returns(Task.FromResult(MockContactFeatureCreate()));
        // Act
        var result = await _contactFeaturesController.CreateContactFeature(new ContactFeatureCreateCommandRequest());
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objResult = (OkObjectResult)result;
        var contactResult = (ContactFeatureCreateCommandResponse)objResult.Value;
        Assert.NotNull(contactResult);
        (result as OkObjectResult).StatusCode.Should().Be(200);

    }
    [Fact]
    //delete
    public async Task RemoveContactFeature_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        _mediator.Setup(_ => _.Send(It.IsAny<ContactFeatureDeleteCommandRequest>(), It.IsAny<CancellationToken>()))
          .Returns(Task.FromResult(MockContactFeatureDelete()));
        // Act
        var result = await _contactFeaturesController.RemoveContactFeature(new ContactFeatureDeleteCommandRequest());
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objResult = (OkObjectResult)result;
        var contactResult = (ContactFeatureDeleteCommandResponse)objResult.Value;
        Assert.True(contactResult.Success);
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    private static ContactFeatureDeleteCommandResponse MockContactFeatureDelete()
    {
        return new ContactFeatureDeleteCommandResponse()
        {
            Success = true,
            Message = "Contact feature deleted successfully"
        };
    }

    private static ContactFeatureCreateCommandResponse MockContactFeatureCreate()
    {
       return new ContactFeatureCreateCommandResponse()
       {
            Success = true,
            Message = "Contact feature created successfully"
        };
    }

    #endregion



    private ContactUpdateCommandResponse UpdateMockResponse()
    {
        return new ContactUpdateCommandResponse()
        {
            Success = true,
            Message = "Contact updated successfully"
        };
    }

    private ContactDeleteCommandResponse DeleteMockResponse()
    {
        return new ContactDeleteCommandResponse()
        {
            Success = true,
            Message = "Contact deleted successfully"
        };
    }

    private BaseResponse<GetContactByIdQueryResponse> GetMockContactById(string contactId1)
    {
        return new BaseResponse<GetContactByIdQueryResponse>
        {
            Data = new GetContactByIdQueryResponse()
            {
                Id =Guid.Parse(contactId1),
                FirstName = "test",
                LastName = "test",
                Company ="test",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.MinValue
            }
        };
    }

    private static BaseResponse<ContactCreateCommandResponse> MockContactCreate()
    {
        return new BaseResponse<ContactCreateCommandResponse>
        {
            Data = new ContactCreateCommandResponse()
            {
                FirstName = "test",
                LastName = "test",
                Company ="test",
                Id = Guid.NewGuid()
            }
        };
    }

    private BasePaginationResponse<GetContactsQueryResponse> GetMockContactList()
    {
        return new BasePaginationResponse<GetContactsQueryResponse>
        {
            Data = new GetContactsQueryResponse()
            {
                Contacts = new object()
                {
                }
            }
        };
    }
}