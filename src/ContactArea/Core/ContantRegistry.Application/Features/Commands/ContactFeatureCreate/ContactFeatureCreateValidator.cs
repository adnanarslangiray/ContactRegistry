using FluentValidation;

namespace ContantRegistry.Application.Features.Commands.ContactFeatureCreate;

public class ContactFeatureCreateValidator : AbstractValidator<ContactFeatureCreateCommandRequest>
{
    public ContactFeatureCreateValidator()
    {
        RuleFor(x => x.ContactId).NotEmpty().NotNull().MaximumLength(50);

        RuleFor(x => x.ContactFeatureType).NotEmpty().NotNull().InclusiveBetween(1, 3);// 1 = Email, 2 = Phone, 3 = Location

        RuleFor(x => x.ContactFeatureValue).NotEmpty().NotNull().MaximumLength(50);
    }
}