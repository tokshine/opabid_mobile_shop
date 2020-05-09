using System.Collections.Generic;

namespace Shivonet.MobileShop.API.Models
{
    public class Order
    {
        public string OrderId { get; set; }

        public decimal OrderTotal { get; set; }

        public List<Product> Products { get; set; }

        public Address Address { get; set; }

        public string UserId { get; set; }
    }
}
