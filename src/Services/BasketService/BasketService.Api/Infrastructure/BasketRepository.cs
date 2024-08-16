using BasketService.Api.Core.Application.Repository;
using BasketService.Api.Core.Domain;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Api.Infrastructure
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public BasketRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            var redisValue = await _database.StringGetAsync(customerId);

            if (redisValue.IsNullOrEmpty)
            {
                return null;
            }

            var cb = JsonConvert.DeserializeObject<CustomerBasket>(redisValue);

            return cb;
        }

        public IEnumerable<string> GetUsers()
        {
            var ep = _redis.GetEndPoints();
            var server = _redis.GetServer(ep.First());
            if (server == null)
            {
                return null;
            }

            var keyList = server.Keys();

            return keyList.Select(a => a.ToString());
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var cb = await _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));

            if (!cb)
            {
                return null;
            }

            return await GetBasketAsync(basket.BuyerId);
        }
    }
}
