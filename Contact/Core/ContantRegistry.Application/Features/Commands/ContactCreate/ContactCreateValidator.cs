using FluentValidation;

namespace ContantRegistry.Application.Features.Commands.ContactCreate;

public class ContactCreateValidator : AbstractValidator<ContactCreateCommandRequest>
{
    public ContactCreateValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.Company).NotEmpty().NotNull().MaximumLength(50);
    }
}