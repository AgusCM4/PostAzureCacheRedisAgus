using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAzureCacheRedisAgus.Helpers
{
    public static class CacheRedisMultiplexer
    {
        private static Lazy<ConnectionMultiplexer> CreateConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("postazureagustajamar.redis.cache.windows.net:6380,password=zpH6jq5QLCKWKaeBvvlD4Xfn4zImPre66AzCaKOPMQI=,ssl=True,abortConnect=False");
            });

        public static ConnectionMultiplexer GetConnection
        {
            get
            {
                return CreateConnection.Value;
            }
        }
    }
}
