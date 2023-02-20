using Common.Caching.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Caching
{
    public abstract class BaseCache : IBaseCache
    {
        public abstract bool Add(string key, object value, TimeSpan expiresAt);

        public abstract bool Add<T>(string key, T value, TimeSpan expiresAt) where T : class;

        public abstract bool Exists(string key);

        public abstract T? Get<T>(string key) where T : class;

        public abstract List<T> GetList<T>(string key) where T : class;

        public abstract bool Remove(string key);

        public abstract void Remove(string[] keys);

        public abstract string SerializeContent(object value);

        public abstract void Stop();

        public abstract bool Update<T>(string key, T value) where T : class;
    }
}
