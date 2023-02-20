using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Caching.Providers.Redis
{
    public class RedisHelper : BaseCache
    {
        private static readonly Lazy<ConnectionMultiplexer> _lazyConnection;
        static RedisHelper()
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });
        }
        static ConnectionMultiplexer Connection
        {
            get { return _lazyConnection.Value; }
        }

        static IDatabase GetDatabase
        {
            get { return Connection.GetDatabase(); }
        }
        public override bool Add(string key, object value, TimeSpan expiresAt)
        {
            throw new NotImplementedException();
        }

        public override bool Add<T>(string key, T value, TimeSpan expiresAt)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public override T? Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public override List<T> GetList<T>(string key)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public override void Remove(string[] keys)
        {
            throw new NotImplementedException();
        }

        public override string SerializeContent(object value)
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override bool Update<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        private string SerializeObject<T>(List<T> list)
        {
            return JsonConvert.SerializeObject(list);
        }
    }
}
