﻿using Akavache;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Shivonet.MobileShop.Core.Models;

namespace Shivonet.MobileShop.Core.Services.Data
{
    public class BaseService
    {
        protected IBlobCache Cache;

        public BaseService(IBlobCache cache)
        {
            Cache = cache ?? BlobCache.LocalMachine;
        }

        public async Task<T> GetFromCache<T>(string cacheName)
        {
            try
            {
                T t = await Cache.GetObject<T>(cacheName);
                return t;
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        public void InvalidateCache()
        {
            Cache.InvalidateAllObjects<Product>();
        }
    }
}
