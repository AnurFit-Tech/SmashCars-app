using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog;
public class SupplierEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SupplierID { get; set; }
    public string SupplierName { get; set; }
    public string Country { get; set; }
    public string ContactInfo { get; set; }
}