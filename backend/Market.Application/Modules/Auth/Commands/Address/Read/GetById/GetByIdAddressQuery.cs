namespace Market.Application.Modules.Auth.Commands.Address.Read.GetById;

public sealed class GetByIdAddressQuery : IRequest<GetByIdAddressQueryDto>
{
    public int AddressID { get; set; }
}



