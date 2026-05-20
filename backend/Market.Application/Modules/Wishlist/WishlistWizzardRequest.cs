using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.Modules.Wishlist
{
    public class WishlistWizzardRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Priority { get; set; } = "Medium"; 
        public int Quantity { get; set; } = 1;
        public string Note { get; set; } = string.Empty;
    }
}
