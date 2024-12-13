using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using talabat.Core.Services;

namespace Talabat.Service
{
    public class CashingService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public CashingService(IConnectionMultiplexer Redis)
        {
            _database=Redis.GetDatabase();
        }
        public async Task CashResponse(string CashKey, object Response, TimeSpan Expiretime)
        {
            if (Response is null) return;
            var Option = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var SerialzationResponse=JsonSerializer.Serialize(Response, Option);
           await  _database.StringSetAsync(CashKey,SerialzationResponse,Expiretime);
        }

        public async Task<string?> GetCashResponse(string CashKey)
        {
          var CashedResponse=  await _database.StringGetAsync(CashKey);
            if (CashedResponse.IsNullOrEmpty) return null;
            return CashedResponse;


        }
    }
}
