// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using TestStuff;
using TestStuff.Product;

Console.WriteLine("Hello, World!");
const string ProductCacheKey = "AllProducts";

 void ReadDataFromRedis()
{
    var cache = RedisHelper.Connection.GetDatabase();

    var serializedObject = cache.StringGet(ProductCacheKey);

    var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(serializedObject);

    foreach (var product in products)
    {
        Console.WriteLine(product.Id + " " + product.Name + " " + product.Price);
    }
}

void SaveDataToRedis()
{
    var cache = RedisHelper.Connection.GetDatabase();

    var service = new ProductService();

    var products = service.GetProducts();

    var serializedObject = JsonConvert.SerializeObject(products);

    cache.StringSet(ProductCacheKey, serializedObject);
}

SaveDataToRedis();

ReadDataFromRedis();