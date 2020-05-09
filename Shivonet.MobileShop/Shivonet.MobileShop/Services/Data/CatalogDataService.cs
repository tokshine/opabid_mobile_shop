using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Akavache;
using Shivonet.MobileShop.Core.Constants;
using Shivonet.MobileShop.Core.Contracts.Services.Data;
using Shivonet.MobileShop.Core.Extensions;
using Shivonet.MobileShop.Core.Models;
using System.Reactive.Linq;
using Shivonet.MobileShop.Core.Contracts.Repository;

namespace Shivonet.MobileShop.Core.Services.Data
{
    public class CatalogDataService : BaseService, ICatalogDataService
    {
        private readonly IGenericRepository _genericRepository;

        public CatalogDataService(IGenericRepository genericRepository, 
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            List<Product> productsFromCache = 
                await GetFromCache<List<Product>>(CacheNameConstants.AllProducts);

            if (productsFromCache != null)//loaded from cache
            {
                return productsFromCache;
            }
            else
            {
                UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
                {
                    Path = ApiConstants.CatalogEndpoint
                };

                var products = await _genericRepository.GetAsync<List<Product>>(builder.ToString());

                await Cache.InsertObject(CacheNameConstants.AllProducts, products, DateTimeOffset.Now.AddSeconds(20));

                return products;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsOfTheWeekAsync()
        {
            List<Product> productsFromCache = await GetFromCache<List<Product>>(CacheNameConstants.ProductOfTheWeek);

            if (productsFromCache != null)//loaded from cache
            {
                return productsFromCache;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.ProductsOfTheWeekEndpoint
            };

            var products = await _genericRepository.GetAsync<List<Product>>(builder.ToString());

            await Cache.InsertObject(CacheNameConstants.ProductOfTheWeek, products, DateTimeOffset.Now.AddSeconds(20));

            return products;
        }
    }
}
