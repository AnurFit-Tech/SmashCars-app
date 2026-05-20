namespace Market.Application.Modules.Auth.Commands.Address.Read.GetByFilter;

public sealed class GetByFilterAddressQuery : IRequest<List<GetByFilterAddressQueryDto>>
{
    public int? UserID { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
}
