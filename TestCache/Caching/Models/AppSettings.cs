using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCache.Caching.Models
{
    public class AppSettings
    {
        public Caching Caching { get; set; }
    }

    public class BaseCache
    {
        public string ConnectionString { get; set; }
    }

    public class Redis : BaseCache
    {

    }

    public class Caching
    {
        public Redis Redis { get; set; }
    }
}
