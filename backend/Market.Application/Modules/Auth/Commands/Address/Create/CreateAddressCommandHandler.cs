namespace Market.Application.Modules.Auth.Commands.Address.Create;

public sealed class CreateAddressCommandHandler(
    IAppDbContext ctx)
    : IRequestHandler<CreateAddressCommand, CreateAddressCommandDto>
{
    public async Task<CreateAddressCommandDto> Handle(CreateAddressCommand request, CancellationToken ct)
    {
        var exists = await ctx.Address.AnyAsync(x => x.UserID == request.UserID, ct);
        if (exists)throw new InvalidOperationException("Korisnik već ima adresu. Možete je samo izmijeniti.");

        var address = new AddressEntity
        {
            UserID = request.UserID,
            Street = request.Street.Trim(),
            City = request.City?.Trim(),
            Country = request.Country?.Trim(),
            PostalCode = request.PostalCode?.Trim(),
            AdditionalInfo = request.AdditionalInfo?.Trim()
        };

        ctx.Address.Add(address);
        await ctx.SaveChangesAsync(ct);

        return new CreateAddressCommandDto{AddressID = address.AddressID
        };
    }
}
