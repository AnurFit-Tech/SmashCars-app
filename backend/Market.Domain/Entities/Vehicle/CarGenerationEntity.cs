namespace Market.Domain.Entities.Vehicle;

public class CarGenerationEntity
{
    public int Id { get; set; }

    public int CarBrandId { get; set; }
    public CarBrandEntity CarBrand { get; set; }

    public string Name { get; set; } =null!;// Golf 6
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }

    public int? EngineSizeCC { get; set; }
    public string FuelType { get; set; }
}