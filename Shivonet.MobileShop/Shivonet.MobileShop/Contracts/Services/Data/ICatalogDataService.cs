using System.Collections.Generic;
using System.Threading.Tasks;
using Shivonet.MobileShop.Core.Models;

namespace Shivonet.MobileShop.Core.Contracts.Services.Data
{
    public interface ICatalogDataService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsOfTheWeekAsync();
    }
}
