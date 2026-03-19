namespace Market.Application.Modules.Catalog.Products.Commands.Update;

public sealed class UpdateInlineProductCommand : IRequest<Unit>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
