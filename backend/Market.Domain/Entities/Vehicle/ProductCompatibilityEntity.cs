using Market.Domain.Entities.Catalog;

namespace Market.Domain.Entities.Vehicle;
public class ProductCompatibilityEntity
{
    public int ProductId { get; set; }
    public ProductEntity Product { get; set; }

    public int CarGenerationId { get; set; }
    public CarGenerationEntity CarGeneration { get; set; }
}