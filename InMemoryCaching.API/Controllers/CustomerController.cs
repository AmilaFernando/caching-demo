using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InMemoryCaching.API.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace InMemoryCaching.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly ApplicationDbContext context;
        private readonly IDistributedCache distributedCache;

        public CustomerController(IMemoryCache memoryCache, ApplicationDbContext context, IDistributedCache distributedCache)
        {
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var cacheKey = "customers";
            if (!memoryCache.TryGetValue(cacheKey, out IList<Customer> customers))
            {
                customers = await context.Customers.ToListAsync();
                
                var cacheExpirationOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };

                memoryCache.Set(cacheKey, customers, cacheExpirationOptions);
            }
            return Ok(customers);
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersRedisCache()
        {

            var cacheKey = "customers";
            string serializedCustomerList;
            IList<Customer> customers;

            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customers = JsonConvert.DeserializeObject<List<Customer>>(serializedCustomerList);
            }
            else
            {
                customers = await context.Customers.ToListAsync();

                // 1000 records are too much
                customers = customers.Take(10).ToList();

                serializedCustomerList = JsonConvert.SerializeObject(customers);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(customers);
        }

    }
}