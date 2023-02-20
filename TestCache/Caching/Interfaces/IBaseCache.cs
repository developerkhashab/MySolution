namespace TestCache.Caching.Interfaces
{
    public interface IBaseCache
    {
        bool Remove(string key);
        void Remove(string[] keys);
        bool Exists(string key);
        void Stop();
        bool Add(string key, object value);
        bool Add<T>(string key, T value) where T : class;
        bool Update<T>(string key, T value) where T : class;
        bool UpdateItemInList<T>(string key, T value, string propertyToBeUpdatedBy) where T : class;
        T? Get<T>(string key) where T : class;
        List<T> GetList<T>(string key) where T : class;
        string SerializeContent(object value);
        T DeserializeContent<T>(string myString);
    }
}
