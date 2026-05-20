using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market.Domain.Common;
using Market.Domain.Entities.Catalog;
namespace Market.Domain.Entities.Sales
{
    public class WishlistProductEntity  : BaseEntity

    {        
        public int WishlistId { get; set; }
        public WishlistEntity Wishlist { get; set; }

        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}
