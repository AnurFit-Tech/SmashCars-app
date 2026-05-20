namespace Market.Application.Modules.Auth.Commands.Address.Create;

public sealed class CreateAddressCommand : IRequest<CreateAddressCommandDto>
{
    public int UserID { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public string AdditionalInfo { get; set; }

}
