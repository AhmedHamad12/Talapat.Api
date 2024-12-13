using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using talabat.Core.Models;
using talabat.Core.Repository;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer Redis)
        {
            _database = Redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetCustomerAsync(string BasketId)
        {
           var Basket=await _database.StringGetAsync(BasketId);
            if (Basket.IsNull)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateCustomerAsync(CustomerBasket Basket)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);
            var CreateOrUpdate=  await _database.StringSetAsync(Basket.id, JsonBasket,TimeSpan.FromMinutes(24*60));
            if (!CreateOrUpdate)
                return null;
            else 
                return await GetCustomerAsync(Basket.id);
        }
        public Task<bool> DeleteBasketAsync(string BasketId)
        {
            return _database.KeyDeleteAsync(BasketId);
        }
    }
}
