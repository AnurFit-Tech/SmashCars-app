using Market.Application.Modules.Auth.Commands.Address.Read.GetById;
namespace Market.Application.Modules.Auth.Commands.Address.Read;

public sealed class GetByIdAddressQueryHandler(
    IAppDbContext ctx)
    : IRequestHandler<GetByIdAddressQuery, GetByIdAddressQueryDto>
{
    public async Task<GetByIdAddressQueryDto> Handle(GetByIdAddressQuery request, CancellationToken ct)
    {
        var address = await ctx.Address.FirstOrDefaultAsync(x => x.AddressID == request.AddressID, ct);

        if (address == null) throw new InvalidOperationException("Adresa s tim ID-em ne postoji.");

        return new GetByIdAddressQueryDto
        {
            AddressID=address.AddressID,
            UserID = address.UserID,
            Street = address.Street,
            City = address.City,
            Country = address.Country,
            PostalCode = address.PostalCode,
            AdditionalInfo = address.AdditionalInfo
        };
    }
}
