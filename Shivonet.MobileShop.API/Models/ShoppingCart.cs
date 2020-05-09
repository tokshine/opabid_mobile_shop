using System.Collections.Generic;

namespace Shivonet.MobileShop.API.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public string UserId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
