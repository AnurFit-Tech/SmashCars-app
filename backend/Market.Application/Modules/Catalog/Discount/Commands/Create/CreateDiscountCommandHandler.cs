namespace Market.Application.Modules.Catalog.Discount.Commands.Create;

public class CreateDiscountCommandHandler(IAppDbContext ctx)
    : IRequestHandler<CreateDiscountCommand, int>
{
    public async Task<int> Handle(CreateDiscountCommand request, CancellationToken ct)
    {
        // Validacija DiscountPercent
        if (request.DiscountPercent <= 0 || request.DiscountPercent > 100)
        {
            throw new ValidationException("DiscountPercent must be greater than 0 and less than or equal to 100.");
        }

        // Provjeri postoji li proizvod
        var product = await ctx.Products
            .Where(p => p.Id == request.ProductID)
            .FirstOrDefaultAsync(ct);

        if (product is null)
        {
            throw new ValidationException("Invalid ProductID.");
        }

        // Kreiranje Discount entity
        var discount = new DiscountEntity
        {
            ProductID = request.ProductID,
            DiscountPercent = request.DiscountPercent,
            StartDate = null, // po defaultu, možeš kasnije dodati
            EndDate = null
        };

        ctx.Discount.Add(discount);
        await ctx.SaveChangesAsync(ct);

        return discount.DiscountID;
    }
}
