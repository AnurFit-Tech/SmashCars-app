using Market.Application.Modules.Auth.Commands.Address.Read.GetById;

public sealed class GetByIdAddressQueryValidator : AbstractValidator<GetByIdAddressQuery>
{
    public GetByIdAddressQueryValidator()
    {
        RuleFor(x => x.AddressID)
            .GreaterThan(0).WithMessage("AddressID mora biti veći od 0.");
    }
}
