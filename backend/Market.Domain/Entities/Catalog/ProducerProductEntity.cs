using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Market.Domain.Entities.Catalog;

namespace Market.Domain.Entities.Catalog;

public class ProducerProductEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProducerProductId { get; set; }
    public int ProducerID { get; set; }
    public ProducerEntity Producer { get; set; }
    public int ProductID { get; set; }
    public ProductEntity Product { get; set; }
}
