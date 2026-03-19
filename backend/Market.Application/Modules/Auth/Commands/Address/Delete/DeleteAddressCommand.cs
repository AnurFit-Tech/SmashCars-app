namespace Market.Application.Modules.Auth.Commands.Address.Delete;

public sealed class DeleteAddressCommand : IRequest<DeleteAddressCommandDto>
{
    public int AddressID { get; set; }
}



