using Market.Application.Modules.Auth.Commands.Address.Read.GetAll;
namespace Market.Application.Modules.Auth.Commands.Address.Read;

public sealed class GetAllAddressQueryHandler(
    IAppDbContext ctx)
    : IRequestHandler<GetAllAddressQuery, List<GetAllAddressQueryDto>>
{
    public async Task<List<GetAllAddressQueryDto>> Handle(GetAllAddressQuery request, CancellationToken ct)
    {
        var addresses = await ctx.Address
            .Include(x => x.User) 
            .ToListAsync(ct);

        var result = addresses.Select(address => new GetAllAddressQueryDto
        {
            AddressID=address.AddressID,  
            UserID = address.UserID,
            Street = address.Street,
            City = address.City,
            Country = address.Country,
            PostalCode = address.PostalCode,
            AdditionalInfo = address.AdditionalInfo
        }).ToList();

        return result;
    }
}
