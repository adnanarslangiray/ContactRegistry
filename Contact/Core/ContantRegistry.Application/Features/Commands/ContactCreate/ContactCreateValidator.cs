using FluentValidation;

namespace ContantRegistry.Application.Features.Commands.ContactCreate;

public class ContactCreateValidator : AbstractValidator<ContactCreateCommandRequest>
{
    public ContactCreateValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(5);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(5);
        RuleFor(x => x.Company).NotEmpty().MaximumLength(2);
    }
}