namespace Market.Application.Modules.Auth.Commands.Address.Read.GetByFilter;

public sealed class GetByFilterAddressQueryDto
{
    public int UserID { get; set; }
    public string Street { get; set; } = string.Empty;
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? AdditionalInfo { get; set; }
}
