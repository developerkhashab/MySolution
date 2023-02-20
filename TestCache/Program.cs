// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
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
    }
}

SaveDataToRedis();

ReadDataFromRedis();