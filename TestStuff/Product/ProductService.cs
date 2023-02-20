using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStuff.Product
{
    public class ProductService
    {
        public IEnumerable<Product> GetProducts()
        {
            return new Product[]
            {
                new Product() { Id = 1, Name = "Abc", Price = 100 },
                new Product() { Id = 2, Name = "Def", Price = 200 },
                new Product() { Id = 3, Name = "Ghi", Price = 300 },
                new Product() { Id = 4, Name = "Jkl", Price = 400 },
                new Product() { Id = 5, Name = "Mno", Price = 500 },
            };
        }
    }
}
