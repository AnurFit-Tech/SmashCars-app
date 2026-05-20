using Market.Application.Modules.Auth.Commands.Address.Delete;
namespace Market.Application.Modules.Auth.Commands.Address;

public sealed class DeleteAddressCommandHandler(
    IAppDbContext ctx)
    : IRequestHandler<DeleteAddressCommand, DeleteAddressCommandDto>
{
    public async Task<DeleteAddressCommandDto> Handle(DeleteAddressCommand request, CancellationToken ct)
    {
        var address = await ctx.Address.FirstOrDefaultAsync(x => x.AddressID == request.AddressID, ct);
        if (address == null) throw new InvalidOperationException("Adresa s tim ID-em ne postoji.");

        ctx.Address.Remove(address);
        await ctx.SaveChangesAsync(ct);

        return new DeleteAddressCommandDto();
    }
}
