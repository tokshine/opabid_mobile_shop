using System.Threading.Tasks;
using Shivonet.MobileShop.Core.Models;

namespace Shivonet.MobileShop.Core.Contracts.Services.Data
{
    public interface IShoppingCartDataService
    {
        Task<ShoppingCart> GetShoppingCart(string userId);
        Task<ShoppingCartItem> AddShoppingCartItem(ShoppingCartItem shoppingCartItem, string userId);
    }
}
