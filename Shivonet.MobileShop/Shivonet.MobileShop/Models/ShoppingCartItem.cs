namespace Shivonet.MobileShop.Core.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Total => Quantity * Product.Price;
    }
}
