using TestCache.Caching.Interfaces;
using TestCache.Utility;

namespace TestCache.Caching
{
    public abstract class BaseCache : IBaseCache
    {
        #region Virtual Methods
        public virtual bool Add(string key, object value)
        {
            string errors = string.Empty;

            if (!Guard.KeyValueValid(key, value, out errors))
            {
                throw new ArgumentException(errors);
            }
            return true;
        }

        public virtual bool Add<T>(string key, T value) where T : class
        {
            string errors = string.Empty;

            if (!Guard.KeyValueValid(key, value, out errors))
            {
                throw new ArgumentException(errors);
            }
            return true;
        }

        public virtual bool Exists(string key)
        {
            if (!key.IsNotWhiteSpaceOrEmpty())
            {
                throw new ArgumentException("Key must not be empty");
            }
            return true;
        }

        public virtual T? Get<T>(string key) where T : class
        {
            if (!key.IsNotWhiteSpaceOrEmpty())
            {
                throw new ArgumentException("Key must not be empty");
            }
            return default(T?);
        }

        public virtual List<T> GetList<T>(string key) where T : class
        {
            if (!key.IsNotWhiteSpaceOrEmpty())
            {
                throw new ArgumentException("Key must not be empty");
            }
            return default(List<T>);
        }

        public virtual bool Remove(string key)
        {
            if (!key.IsNotWhiteSpaceOrEmpty())
            {
                throw new ArgumentException("Key must not be empty");
            }
            return true;
        }
        public virtual bool Update<T>(string key, T value) where T : class
        {
            string errors = string.Empty;

            if (!Guard.KeyValueValid(key, value, out errors))
            {
                throw new ArgumentException(errors);
            }
            return true;
        }
        #endregion

        #region Abstract Methods
        public abstract void Remove(string[] keys);

        public abstract string SerializeContent(object value);

        public abstract T DeserializeContent<T>(string myString);

        public abstract void Stop();

        public abstract bool UpdateItemInList<T>(string key, T value, string propertyToBeUpdatedBy) where T : class;

        public abstract void ClearAllDatabases();
        #endregion        
    }
}
