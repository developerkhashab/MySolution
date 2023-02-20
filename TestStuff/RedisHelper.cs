using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStuff
{
    public class RedisHelper
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
            get { return Connection.GetDatabase();  }
        }

        public bool Add(string key, string value)
        {
            var serializedObject = JsonConvert.SerializeObject(value);

            return GetDatabase.StringSet(key, serializedObject);
        }
    }
}
