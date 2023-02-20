using Newtonsoft.Json;
using StackExchange.Redis;
using System.Reflection;
using TestCache.Caching.Interfaces;

namespace TestCache.Caching.Providers.Redis
{
    public class Redis : BaseCache, IBaseCache
    {
        #region Private Readonly
        private static readonly Lazy<ConnectionMultiplexer> _lazyConnection;
        #endregion

        #region Constructor
        static Redis()
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });
        }
        #endregion

        #region Properties
        static ConnectionMultiplexer Connection
        {
            get { return _lazyConnection.Value; }
        }

        static IDatabase _db
        {
            get { return Connection.GetDatabase(); }
        }
        #endregion

        #region Override Methods
        public override bool Exists(string key)
        {
            return _db.KeyExists(key);
        }

        public override bool Add(string key, object value)
        {
            var stringContent = SerializeContent(value);
            return _db.StringSet(key, stringContent);
        }

        public override bool Add<T>(string key, T value)
        {
            var stringContent = SerializeContent(value);
            return _db.StringSet(key, stringContent);
        }

        public override T? Get<T>(string key) where T : class
        {
            try
            {
                RedisValue myString = _db.StringGet(key);
                if (myString.HasValue && !myString.IsNullOrEmpty)
                {
                    return DeserializeContent<T>(myString);
                }

                return null;
            }
            catch (Exception)
            {
                // Log Exception
                return null;
            }
        }

        public override List<T> GetList<T>(string key)
        {
            List<T> allValues = new();

            try
            {
                var endPoints = Connection.GetEndPoints();

                foreach (var ep in endPoints)
                {
                    var server = Connection.GetServer(ep);
                    var keys = server.Keys(_db.Database, key);
                    var keyValues = _db.StringGet(keys.ToArray());

                    var values = (from redisValue in keyValues
                                  where redisValue.HasValue && !redisValue.IsNullOrEmpty
                                  select DeserializeContent<T>(redisValue)).ToList();

                    if (allValues == null)
                        allValues = new List<T>();

                    allValues.AddRange(values);
                }

                return allValues;
            }
            catch (Exception)
            {
                // Log Exception
                return allValues;
            }
        }

        public override bool Remove(string key)
        {
            return _db.KeyDelete(key);
        }

        public override void Remove(string[] keys)
        {
            foreach (var key in keys)
            {
                Remove(key);
            }
        }

        public override bool Update<T>(string key, T value)
        {
            var stringContent = SerializeContent(value);
            return _db.StringSet(key, stringContent);
        }


        // if we want to update row in a list, we can use this function, key is the key of the list, itemtobeupdated is the model,
        // propertyToBeUpdatedBy is the unique property in the model, like productId
        public override bool UpdateItemInList<T>(string key, T itemToBeUpdated, string propertyToBeUpdatedBy)
        {
            // get list from redis
            var list = GetList<T>(key);

            // get property from type
            PropertyInfo pinfo = typeof(T).GetProperty(propertyToBeUpdatedBy) ?? null;
            if (pinfo != null && string.IsNullOrEmpty(propertyToBeUpdatedBy))
            {
                // loop on the list
                foreach (var item in list)
                {
                    // get the proprty, like the example above (productId) from the item
                    object theProperty = pinfo.GetValue(item, null);
                    if (theProperty != null)
                    {
                        // get the proprty, like the example above (productId) from the new model
                        object theMatchedProperty = pinfo.GetValue(itemToBeUpdated, null);

                        // match the unique keys
                        if (theMatchedProperty != null && theProperty == theMatchedProperty)
                        {
                            // replace values in properties
                            PropertyInfo[] properties = typeof(T).GetProperties();
                            foreach (PropertyInfo property in properties)
                            {
                                var newValue = property.GetValue(itemToBeUpdated, null);
                                property.SetValue(item, newValue);
                            }
                            break;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public override string SerializeContent(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public override T DeserializeContent<T>(string myString)
        {
            var redisValue = (RedisValue)myString;
            return JsonConvert.DeserializeObject<T>(redisValue!);
        }

        public override void Stop()
        {
            Connection.Dispose();
        }
        #endregion
    }
}
