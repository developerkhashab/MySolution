// See https://aka.ms/new-console-template for more information
using TestCache.Caching;
using TestStuff.Product;

Console.WriteLine("Hello, World!");


void ReadDataFromRedis()
{
    var redis = new CachingManager();
    var serializedProducts = redis.GetList<Product>("Product.*");

    foreach (var product in serializedProducts)
    {
        Console.WriteLine(product.Id + " " + product.Name + " " + product.Price);
    }
}

void SaveDataToRedis()
{
    var redis = new CachingManager();

    var service = new ProductService();

    var products = service.GetProducts();

    foreach (var item in products)
    {
        redis.Add("Product." + item.Id, item);
        redis.Add("khashab." + item.Id, item);
        redis.Add("ttttttt." + item.Id, item);
    }
}

SaveDataToRedis();

ReadDataFromRedis();

