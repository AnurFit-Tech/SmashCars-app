using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog;
public class ProducerEntity 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProducerID { get; set; }
    public string ProducerName { get; set; }
    public string Country { get; set; }
    public string ContactInfo { get; set; }
}