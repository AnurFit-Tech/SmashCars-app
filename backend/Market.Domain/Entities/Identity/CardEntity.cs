using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Market.Domain.Common;
using Market.Domain.Entities.Catalog;

namespace Market.Domain.Entities.Identity;

public sealed class CardEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CardID { get; set; }
    public int UserID { get; set; }
    public UserEntity User {get; set;}
    public string CardNumber { get; set; }
    public string? CardHolderName { get; set; }
    public string? CardType { get; set; }
    public DateOnly? ExpirationDate { get; set; }

}