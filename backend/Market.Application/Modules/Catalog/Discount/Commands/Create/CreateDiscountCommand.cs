namespace Market.Application.Modules.Catalog.Discount.Commands.Create;

public class CreateDiscountCommand : IRequest<int>
{
    public int ProductID { get; set; }
    public decimal DiscountPercent { get; set; }
}