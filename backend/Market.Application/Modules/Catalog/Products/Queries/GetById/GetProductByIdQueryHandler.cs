namespace Market.Application.Modules.Catalog.Products.Queries.GetById;

public class GetProductByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryDto>
{
    public async Task<GetProductByIdQueryDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var q = context.Products
            .Where(c => c.Id == request.Id);

        var dto = await q
            .Select(x => new GetProductByIdQueryDto
            {
                Id = x.Id,
                Name = x.Name,
                IsEnabled = x.IsEnabled,
                Description = x.Description,
                StockQuantity = x.StockQuantity,
                CategoryName = x.Category!.Name,
                CategoryId = x.CategoryId,
                Price = x.Price - (x.Discount
                .Where(d => (d.StartDate == null || d.StartDate <= today) &&
                (d.EndDate == null || d.EndDate >= today))
                .OrderByDescending(d => d.DiscountPercent)
                .Select(d => d.DiscountPercent)
                .FirstOrDefault() / 100 * x.Price)

            })
            .FirstOrDefaultAsync(cancellationToken);

        if (dto == null)
        {
            throw new MarketNotFoundException($"Product with Id {request.Id} not found.");
        }

        return dto;
    }
}