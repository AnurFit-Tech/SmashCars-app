namespace Market.Domain.Entities.Vehicle;
public class CarBrandEntity
{
    
    public int Id { get; set; }
    public string Name { get; set; }=null!;d

    public ICollection<CarGenerationEntity> Generations { get; set; }
}