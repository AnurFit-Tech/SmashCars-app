using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Market.Domain.Entities.Catalog;

namespace Market.Domain.Entities.Catalog;

public class SupplierProductEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SupplierProductId { get; set; }
    public int SupplierID { get; set; }
    public SupplierEntity Supplier { get; set; }
    public int ProductID { get; set; }
    public ProductEntity Product { get; set; }
}
