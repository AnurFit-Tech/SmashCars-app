namespace Market.Application.Modules.Catalog.Products.Commands.Update;

public sealed class UpdateInlineProductCommandHandler : IRequestHandler<UpdateInlineProductCommand, Unit>
{
    private readonly IAppDbContext _ctx;

    public UpdateInlineProductCommandHandler(IAppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Unit> Handle(UpdateInlineProductCommand request, CancellationToken ct)
    {
        // Provjeri da li proizvod postoji
        var entity = await _ctx.Products
            .Where(x => x.Id == request.Id)  // <--- mora ti biti Id property u DTO
            .FirstOrDefaultAsync(ct);

        if (entity is null)
            throw new MarketNotFoundException($"Product (ID={request.Id}) nije pronađen.");

        // Check za duplicate name (case-insensitive, osim za isti ID)
        var exists = await _ctx.Products
            .AnyAsync(x => x.Id != request.Id && x.Name.ToLower() == request.Name.ToLower(), ct);

        if (exists)
        {
            throw new MarketConflictException("Name already exists.");
        }

        // Ažuriramo samo name i price
        entity.Name = request.Name.Trim();
        entity.Price = request.Price;

        await _ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}
