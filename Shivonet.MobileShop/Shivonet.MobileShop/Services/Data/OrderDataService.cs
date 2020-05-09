using System;
using System.Threading.Tasks;
using Shivonet.MobileShop.Core.Constants;
using Shivonet.MobileShop.Core.Contracts.Repository;
using Shivonet.MobileShop.Core.Contracts.Services.Data;
using Shivonet.MobileShop.Core.Models;

namespace Shivonet.MobileShop.Core.Services.Data
{
    public class OrderDataService : IOrderDataService
    {
        private readonly IGenericRepository _genericRepository;

        public OrderDataService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Order> PlaceOrder(Order order)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.PlaceOrderEndpoint
            };

            var result =
                await _genericRepository.PostAsync<Order>(builder.ToString(), order);

            return order;
        }
    }
}
