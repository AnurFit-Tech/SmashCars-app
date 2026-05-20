namespace Market.Application.Modules.Catalog.Products.Queries.List;

public sealed class ListProductsQueryHandler(IAppDbContext ctx)
        : IRequestHandler<ListProductsQuery, PageResult<ListProductsQueryDto>>
{
    public async Task<PageResult<ListProductsQueryDto>> Handle(
        ListProductsQuery request, CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var q = ctx.Products.AsNoTracking();

        var searchTerm = request.Search?.Trim().ToLower() ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
             q = q.Where(x => x.Name.ToLower().Contains(searchTerm));
        }

        var projectedQuery = q.OrderBy(x => x.Name)
            .Select(x => new ListProductsQueryDto
            {
                Id = x.Id,
                Name = x.Name,
                IsEnabled = x.IsEnabled,
                Description = x.Description,
                StockQuantity = x.StockQuantity,
                CategoryName = x.Category!.Name,
                Price = x.Price - (x.Discount
                .Where(d => (d.StartDate == null || d.StartDate <= today) &&
                (d.EndDate == null || d.EndDate >= today))
                .OrderByDescending(d => d.DiscountPercent)
                .Select(d => d.DiscountPercent)
                .FirstOrDefault() / 100 * x.Price)
            });

        return await PageResult<ListProductsQueryDto>.FromQueryableAsync(projectedQuery, request.Paging, ct);
    }


}
