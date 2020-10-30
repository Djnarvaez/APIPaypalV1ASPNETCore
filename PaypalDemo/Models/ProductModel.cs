using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaypalDemo.Models
{
    public class ProductModel
    {
        public List<Product> GetAll()
        {
            return new List<Product>() {

                new Product {
                    Id = 1,
                    Name = "CAMISA VOLCOM",
                    Description = "TEST",
                    Price = 0.01,
                    Quantity = 2
                },
                 new Product {
                    Id = 1,
                    Name = "CAMISA ETNIET",
                    Description = "TEST",
                    Price = 0.01,
                    Quantity = 1
                },
                  new Product {
                    Id = 1,
                    Name = "CAMISA DC",
                    Description = "TEST",
                    Price = 0.01,
                    Quantity = 2
                },
            };
        }
    }
}
