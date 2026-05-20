using FluentValidation;
using Market.Application.Modules.Auth.Commands.Address.Read.GetByFilter;

public sealed class GetByFilterAddressQueryValidator : AbstractValidator<GetByFilterAddressQuery>
{
    public GetByFilterAddressQueryValidator()
    {
        RuleFor(x => x.UserID)
            .GreaterThan(0)
            .WithMessage("UserID mora biti veći od 0.")
            .When(x => x.UserID.HasValue);

        RuleFor(x => x.Street)
            .MaximumLength(150)
            .WithMessage("Street može imati maksimalno 150 karaktera.")
            .When(x => !string.IsNullOrWhiteSpace(x.Street));

        RuleFor(x => x.City)
            .MaximumLength(100)
            .WithMessage("City može imati maksimalno 100 karaktera.")
            .When(x => !string.IsNullOrWhiteSpace(x.City));

        RuleFor(x => x.Country)
            .MaximumLength(100)
            .WithMessage("Country može imati maksimalno 100 karaktera.")
            .When(x => !string.IsNullOrWhiteSpace(x.Country));

        RuleFor(x => x.PostalCode)
            .MaximumLength(20)
            .WithMessage("PostalCode može imati maksimalno 20 karaktera.")
            .When(x => !string.IsNullOrWhiteSpace(x.PostalCode));
    }
}
