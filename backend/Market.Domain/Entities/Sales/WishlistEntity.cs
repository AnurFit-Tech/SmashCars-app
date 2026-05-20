using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market.Domain.Entities.Identity;
using Market.Domain.Common;
namespace Market.Domain.Entities.Sales
{
    public class WishlistEntity : BaseEntity
    {
       // public int Id { get; set; }  ne treba nam jer vec ima u BaseEntity
        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;//Veza sa Userom
        public ICollection<WishlistProductEntity> WishlistProducts { get; set; } = new List<WishlistProductEntity>();
    }
}

