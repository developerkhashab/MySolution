using TestCache.Caching.Interfaces;
using TestCache.Caching.Providers.Redis;

namespace TestCache.Caching
{
    public class CachingManager : IBaseCache
    {
        private readonly IBaseCache _cache;
        public CachingManager()
        {
            _cache = CachingToBeUsed();
        }

        private IBaseCache CachingToBeUsed() 
        { 
            return new Redis(); 
        }

        public bool Add(string key, object value) => _cache.Add(key, value);

        public bool Add<T>(string key, T value) where T : class => _cache.Add<T>(key, value);

        public T DeserializeContent<T>(string myString) => _cache.DeserializeContent<T>(myString);

        public bool Exists(string key) => _cache.Exists(key);

        public T? Get<T>(string key) where T : class => _cache.Get<T>(key);

        public List<T> GetList<T>(string key) where T : class => _cache.GetList<T>(key);

        public bool Remove(string key) => _cache.Remove(key);

        public void Remove(string[] keys) => _cache.Remove(keys);

        public string SerializeContent(object value) => _cache.SerializeContent(value);

        public void Stop() => _cache.Stop();

        public bool Update<T>(string key, T value) where T : class => _cache.Update<T>(key, value);

        public bool UpdateItemInList<T>(string key, T value, string propertyToBeUpdatedBy) where T : class => _cache.UpdateItemInList<T>(key, value, propertyToBeUpdatedBy);

        public void ClearAllDatabases() => _cache.ClearAllDatabases();
    }
}
