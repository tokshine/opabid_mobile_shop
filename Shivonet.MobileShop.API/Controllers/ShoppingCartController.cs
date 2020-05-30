using System.Linq;
using Shivonet.MobileShop.API.Models;
using Shivonet.MobileShop.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shivonet.MobileShop.API.Controllers
{
    [Route("api/[controller]/")]
    public class ShoppingCartController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ShoppingCartController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetItemsForUser(string userId)
        {
            var shoppingCart = _appDbContext.ShoppingCarts.Include(s => s.ShoppingCartItems).ThenInclude(s => s.Product).FirstOrDefault(s => s.UserId == userId);
            return shoppingCart == null ? Ok(new ShoppingCart()) : Ok(shoppingCart);
        }

        [HttpPost]        
        public IActionResult Add([FromBody]UserShoppingCartItem userShoppingCartItem)
        {
            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.UserId == userShoppingCartItem.UserId);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { UserId = userShoppingCartItem.UserId };
                _appDbContext.ShoppingCarts.Add(shoppingCart);
                _appDbContext.SaveChanges();
            }

            var product = _appDbContext.Products.FirstOrDefault(p => p.ProductId == userShoppingCartItem.ShoppingCartItem.ProductId);

            var shoppingCartItem = new ShoppingCartItem
            {
                Product = product,
                ProductId = userShoppingCartItem.ShoppingCartItem.Product.ProductId,
                Quantity = userShoppingCartItem.ShoppingCartItem.Quantity,
                ShoppingCartId = shoppingCart.ShoppingCartId
            };
            _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            _appDbContext.SaveChanges();

            return Ok(shoppingCartItem);
        }
    }
}
