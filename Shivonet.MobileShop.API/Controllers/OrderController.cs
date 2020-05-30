using System.Linq;
using Shivonet.MobileShop.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shivonet.MobileShop.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public OrderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        // GET: /<controller>/
        public IActionResult Add([FromBody]Order order)
        {
            
            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.UserId == order.UserId);
            var shoppingCartItemsToRemove =
                _appDbContext.ShoppingCartItems.Where(s => s.ShoppingCartId == shoppingCart.ShoppingCartId);

            //handling of order not fully implemented in this demo
            //todo create order details table to as a bridge btw order and products purchase
            //orderdetails.Products = shoppingCartItemsToRemove.Select(x => x.Product).ToList();
            //product property should not be on order table
            //orderId property should not be on product table

            _appDbContext.Orders.Add(order);
            _appDbContext.ShoppingCartItems.RemoveRange(shoppingCartItemsToRemove);           
            _appDbContext.SaveChanges();

            return Ok(order);
        }
    }
}
