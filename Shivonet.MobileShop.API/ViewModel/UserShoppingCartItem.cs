using Shivonet.MobileShop.API.Models;

namespace Shivonet.MobileShop.API.ViewModel
{
    public class UserShoppingCartItem
    {
        public string UserId { get; set; }
        public ShoppingCartItem ShoppingCartItem { get; set; }
    }
}
