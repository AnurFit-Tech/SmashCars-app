using Market.Domain.Entities.Sales;
using Market.Domain.Entities.Vehicle;
namespace Market.Application.Abstractions;

// Application layer
public interface IAppDbContext
{
    DbSet<ProductEntity> Products { get; }
    DbSet<ProductCategoryEntity> ProductCategories { get; }
    DbSet<UserEntity> Users { get; }
    DbSet<RefreshTokenEntity> RefreshTokens { get; }

    DbSet<OrderEntity> Orders { get; }
    DbSet<DiscountEntity> Discount { get; }
    DbSet<AddressEntity> Address { get; }
    DbSet<ProducerEntity> Producer { get; }
    DbSet<SupplierEntity> Supplier { get; }
    DbSet<ProducerProductEntity> ProducerProduct { get; }
    DbSet<SupplierProductEntity> SupplierProduct { get; }
    DbSet<CardEntity> Card { get; }
    DbSet<OrderItemEntity> OrderItems { get; }

    Task<int> SaveChangesAsync(CancellationToken ct);

    DbSet<WishlistEntity> Wishlists { get; }
    DbSet<WishlistProductEntity> WishlistProducts { get; }


     DbSet<CarBrandEntity> CarBrands { get;  }
     DbSet<CarGenerationEntity> CarGenerations { get;  }
     DbSet<ProductCompatibilityEntity> ProductCompatibilities { get;  }
}