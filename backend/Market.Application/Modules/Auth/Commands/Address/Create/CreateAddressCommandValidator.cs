using Market.Application.Modules.Auth.Commands.Address.Create;

public sealed class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(x => x.UserID)
            .GreaterThan(0).WithMessage("UserID mora biti veći od 0.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street je obavezno.")
            .MaximumLength(150).WithMessage("Street može imati maksimalno 150 karaktera.");

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City može imati maksimalno 100 karaktera.")
            .When(x => x.City is not null);

        RuleFor(x => x.Country)
            .MaximumLength(100).WithMessage("Country može imati maksimalno 100 karaktera.")
            .When(x => x.Country is not null);

        RuleFor(x => x.PostalCode)
            .MaximumLength(20).WithMessage("PostalCode može imati maksimalno 20 karaktera.")
            .When(x => x.PostalCode is not null);

        RuleFor(x => x.AdditionalInfo)
            .MaximumLength(250).WithMessage("AdditionalInfo može imati maksimalno 250 karaktera.")
            .When(x => x.AdditionalInfo is not null);
    }
}
