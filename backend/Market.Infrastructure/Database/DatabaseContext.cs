using Market.Application.Abstractions;
using Market.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Market.Domain.Entities.Vehicle;
namespace Market.Infrastructure.Database;

public partial class DatabaseContext : DbContext, IAppDbContext
{
    public DbSet<ProductCategoryEntity> ProductCategories => Set<ProductCategoryEntity>();
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();

    public DbSet<OrderEntity> Orders => Set<OrderEntity>();
    public DbSet<DiscountEntity> Discount => Set<DiscountEntity>();
    public DbSet<ProducerEntity> Producer => Set<ProducerEntity>();
    public DbSet<SupplierEntity> Supplier => Set<SupplierEntity>();
    public DbSet<ProducerProductEntity> ProducerProduct => Set<ProducerProductEntity>();
    public DbSet<SupplierProductEntity> SupplierProduct => Set<SupplierProductEntity>();
    public DbSet<AddressEntity> Address => Set<AddressEntity>();
    public DbSet<CardEntity> Card => Set<CardEntity>();
    public DbSet<OrderItemEntity> OrderItems => Set<OrderItemEntity>();

    private readonly TimeProvider _clock;
    public DatabaseContext(DbContextOptions<DatabaseContext> options, TimeProvider clock) : base(options)
    {
        _clock = clock;
    }
     
    public DbSet<WishlistEntity> Wishlists =>Set<WishlistEntity>();
    public DbSet<WishlistProductEntity> WishlistProducts => Set<WishlistProductEntity>();

    public DbSet<CarBrandEntity> CarBrands => Set<CarBrandEntity>();
    public DbSet<CarGenerationEntity> CarGenerations => Set<CarGenerationEntity>();
    public DbSet<ProductCompatibilityEntity> ProductCompatibilities => Set<ProductCompatibilityEntity>();




}