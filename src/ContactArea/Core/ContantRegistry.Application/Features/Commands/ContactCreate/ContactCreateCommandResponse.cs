using ContantRegistry.Application.DTOs;

namespace ContantRegistry.Application.Features.Commands.ContactCreate;

public class ContactCreateCommandResponse : BaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }
}