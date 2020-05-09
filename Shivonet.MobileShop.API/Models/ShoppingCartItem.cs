using System.ComponentModel.DataAnnotations;

namespace Shivonet.MobileShop.API.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int ShoppingCartItemId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
