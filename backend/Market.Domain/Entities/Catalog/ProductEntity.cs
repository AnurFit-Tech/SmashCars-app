using Market.Domain.Common;
using Market.Domain.Entities.Sales;

namespace Market.Domain.Entities.Catalog;

/// <summary>
/// Represents a product in the system.
/// </summary>
public class ProductEntity : BaseEntity
{
   
    public string Name { get; set; }

    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public ProductCategoryEntity? Category { get; set; }

    public bool IsEnabled { get; set; }
    public ICollection<UserProductFavoriteEntity> FavoritedByUsers { get; private set; } = new List<UserProductFavoriteEntity>();
    ///// </summary>
    //public IReadOnlyCollection<OrderItemEntity> Items { get; set; } = new List<OrderItemEntity>();
    public ICollection<DiscountEntity> Discount { get; set; } = new List<DiscountEntity>();
    
    public ICollection<WishlistProductEntity> WishlistProducts { get; set; } = new List<WishlistProductEntity>();// Povezujemo proizvod sa svim listama želja u kojima se nalazi
    public static class Constraints
    {
        public const int NameMaxLength = 150;

        public const int DescriptionMaxLength = 1000;
    }
}