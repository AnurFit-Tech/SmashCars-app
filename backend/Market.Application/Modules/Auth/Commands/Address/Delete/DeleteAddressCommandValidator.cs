using Market.Application.Modules.Auth.Commands.Address.Delete;

public sealed class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
{
    public DeleteAddressCommandValidator()
    {
        RuleFor(x => x.AddressID)
            .GreaterThan(0).WithMessage("AddressID mora biti veći od 0.");
    }
}
