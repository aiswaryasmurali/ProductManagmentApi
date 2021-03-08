using EasyNetQ;
using Order.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using Order.Messages.Command;
using Order.Messages.Event;
using Order.Messages.Models;
using RabbitSubscriber.Services;
using Microsoft.AspNetCore.Mvc;

namespace RabbitSubscriber
{
   public class Program
    {
        ProductService _ProductServices;
        static void Main(string[] args)
        {  
            
            var messageBus = RabbitHutch.CreateBus("host=localhost");
            messageBus.Subscribe<Product>("SubscriptionId", msg =>
                {
                    Console.WriteLine($"Product :{msg.ProductName} costs {msg.Price}");
                });
            //ProductService _productService = new ProductService();

            Program _program = new Program();
            //_program.GetProducts();
            messageBus.Respond<ReturnProductList, ReturnedProductList>(request => new ReturnedProductList
            {
                ProductList = _program.GetProducts().Take(request.Records).ToList()
            });

            messageBus.Respond<ReturnProductList, ReturnedProductList>(request => new ReturnedProductList
            {
                Product = _program.GetProducts(request.Param)
            }); 
        }
        

        public List<Product> GetProducts()
        {
            _ProductServices = new ProductService();

            var productList1 = new List<Product>();

            productList1 = _ProductServices.Get();

          
            return productList1;
        }

        public Product GetProducts(string productid)
        {
            _ProductServices = new ProductService();

            
            Product product =_ProductServices.Get(productid);
           
           

            return product;
        }
    }
}
