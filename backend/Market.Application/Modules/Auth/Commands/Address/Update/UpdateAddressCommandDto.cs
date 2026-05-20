namespace Market.Application.Modules.Auth.Commands.Address.Update;

public sealed class UpdateAddressCommandDto
{
    public int AddressID { get; set; }
    public int UserID { get; set; }
    public string Street { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? AdditionalInfo { get; set; }
}