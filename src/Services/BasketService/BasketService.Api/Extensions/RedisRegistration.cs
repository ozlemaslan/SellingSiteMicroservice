using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;

namespace BasketService.Api.Extensions
{
    public static class RedisRegistration
    {
        /// <summary>
        /// redise bağlantı
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ConnectionMultiplexer ConfigureRedis(this IServiceProvider services, IConfiguration configuration)
        {
            var redisConf = ConfigurationOptions.Parse(configuration["Redis:ConnectionStr"], true);
            redisConf.ResolveDns = true;

            return ConnectionMultiplexer.Connect(redisConf);
        }
    }
}
