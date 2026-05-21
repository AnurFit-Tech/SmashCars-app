using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Market.Domain.Entities.Vehicle;
namespace Market.Infrastructure.Database.Configurations.Vehicle;
public class ProductCompatibilityConfiguration : IEntityTypeConfiguration<ProductCompatibilityEntity>
{
    public void Configure(EntityTypeBuilder<ProductCompatibilityEntity> builder)
    {
        builder.HasKey(x => new { x.ProductId, x.CarGenerationId });

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.CarGeneration)
            .WithMany()
            .HasForeignKey(x => x.CarGenerationId);
    }
}