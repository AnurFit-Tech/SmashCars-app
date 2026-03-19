using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Market.Domain.Common;
using Market.Domain.Entities.Sales;

namespace Market.Domain.Entities.Catalog;
public class DiscountEntity 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DiscountID { get; set; }
    public int ProductID { get; set; }
    public ProductEntity Product { get; set; }
    public decimal DiscountPercent { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

}