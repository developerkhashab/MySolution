using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Caching.Interfaces
{
    public interface IBaseCache
    {
        bool Remove(string key);
        //void Remove(RedisKey[] keys);
        void Remove(string[] keys);
        bool Exists(string key);
        void Stop();
        bool Add(string key, object value, TimeSpan expiresAt);
        bool Add<T>(string key, T value, TimeSpan expiresAt) where T : class;
        //bool Add<T>(string key, T value, TimeSpan expiresAt, CommandFlags flags) where T : class;
        bool Update<T>(string key, T value) where T : class;
        T? Get<T>(string key) where T : class;
        List<T> GetList<T>(string key) where T : class;

        string SerializeContent(object value);

        //T? DeserializeContent<T>(RedisValue myString);
    }
}
