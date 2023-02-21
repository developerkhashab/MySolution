// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using StackExchange.Redis;
using TestCache.Caching.Providers.Redis;
using TestStuff.Product;

Console.WriteLine("Hello, World!");



Console.WriteLine("Hello, World!");
const string ProductCacheKey = "AllProducts";

void ReadDataFromRedis()
{
    //var cache = RedisHelper.Connection.GetDatabase();

    //var serializedObject = cache.StringGet(ProductCacheKey);
    var redis = new Redis();
    var serializedProducts = redis.GetList<Product>("Product.*");
    serializedProducts.AddRange(redis.GetList<Product>("khashab.*"));
    serializedProducts.AddRange(redis.GetList<Product>("ttttttt.*"));
    Console.WriteLine(redis.Exists("Product.9"));
    Console.WriteLine(redis.Exists("Product.4"));
    Console.WriteLine(redis.Exists("Product.5"));
    redis.Remove("Product.5");
    Console.WriteLine(redis.Exists("Product.5"));

    //var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(serializedObject);
    foreach (var product in serializedProducts)
    {
        Console.WriteLine(product.Id + " " + product.Name + " " + product.Price);
    }
}

void SaveDataToRedis()
{
    var redis = new Redis();

    var service = new ProductService();

    var products = service.GetProducts();

    //var serializedObject = JsonConvert.SerializeObject(products);
    foreach (var item in products)
    {
        redis.Add("Product." + item.Id, item);
        redis.Add("khashab." + item.Id, item);
        redis.Add("ttttttt." + item.Id, item);
    }
}

List<string> GetAllkeys()
{
    List<string> listKeys = new List<string>();
    using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379,allowAdmin=true"))
    {
        var keys = redis.GetServer("localhost", 6379).Keys();
        listKeys.AddRange(keys.Select(key => (string)key).ToList());

    }

    return listKeys;
}

GetAllkeys();

SaveDataToRedis();

ReadDataFromRedis();

