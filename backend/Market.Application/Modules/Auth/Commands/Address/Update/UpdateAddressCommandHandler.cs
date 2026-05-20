using Market.Application.Modules.Auth.Commands.Address.Update;
namespace Market.Application.Modules.Auth.Commands.Address;

public sealed class UpdateAddressCommandHandler(
    IAppDbContext ctx)
    : IRequestHandler<UpdateAddressCommand, UpdateAddressCommandDto>
{
    public async Task<UpdateAddressCommandDto> Handle(UpdateAddressCommand request, CancellationToken ct)
    {
        var address = await ctx.Address.FirstOrDefaultAsync(x => x.AddressID == request.AddressID, ct);

        if (address == null)throw new InvalidOperationException("Adresa s tim ID-em ne postoji.");


        address.Street = request.Street?.Trim() ?? address.Street;
        address.City = request.City?.Trim() ?? address.City;
        address.Country = request.Country?.Trim() ?? address.Country;
        address.PostalCode = request.PostalCode?.Trim() ?? address.PostalCode;
        address.AdditionalInfo = request.AdditionalInfo?.Trim() ?? address.AdditionalInfo;

        await ctx.SaveChangesAsync(ct);

        return new UpdateAddressCommandDto
        {
            AddressID = address.AddressID,
            UserID = address.UserID,
            Street = address.Street,
            City = address.City,
            Country = address.Country,
            PostalCode = address.PostalCode,
            AdditionalInfo = address.AdditionalInfo
        };
    }
}
