namespace Market.Application.Modules.Auth.Commands.Address.Read.GetByFilter;

public sealed class GetFilteredAddressesQueryHandler(
    IAppDbContext ctx)
    : IRequestHandler<GetByFilterAddressQuery, List<GetByFilterAddressQueryDto>>
{
    public async Task<List<GetByFilterAddressQueryDto>> Handle(GetByFilterAddressQuery request, CancellationToken ct)
    {
        var query = ctx.Address.AsQueryable();

        if (request.UserID.HasValue)
            query = query.Where(x => x.UserID == request.UserID.Value);
        if (!string.IsNullOrWhiteSpace(request.Street))
            query = query.Where(x => x.Street.Contains(request.Street));
        if (!string.IsNullOrWhiteSpace(request.City))
            query = query.Where(x => x.City != null && x.City.Contains(request.City));
        if (!string.IsNullOrWhiteSpace(request.Country))
           query = query.Where(x => x.Country != null && x.Country.Contains(request.Country));
        if (!string.IsNullOrWhiteSpace(request.PostalCode))
            query = query.Where(x => x.PostalCode != null && x.PostalCode.Contains(request.PostalCode));

        var addresses = await query.ToListAsync(ct);

        return addresses.Select(address => new GetByFilterAddressQueryDto
        {
            UserID = address.UserID,
            Street = address.Street,
            City = address.City,
            Country = address.Country,
            PostalCode = address.PostalCode,
            AdditionalInfo = address.AdditionalInfo
        }).ToList();
    }
}
