using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using EasyNetQ;
using Newtonsoft.Json;
using Order.Messages;
using Order.Messages.Command;
using Order.Messages.Event;

using Order.Messages.Models;

namespace Rabbit.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        public HttpResponseMessage Post([FromBody] Product order)
        {
            var messageBus = RabbitHutch.CreateBus("host=localhost");

            messageBus.Publish(order);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
        [HttpGet]
        public List<Product> Get()
        {
            var messageBus = RabbitHutch.CreateBus("host=localhost");
            List<Product> _product = new List<Product>();
            var response = messageBus.Request<ReturnProductList, ReturnedProductList>(new ReturnProductList
            {
                Records = 5
            });

            foreach(Product objProduct in response.ProductList)
            {
                
                _product.Add(objProduct);
            }

            return _product;
        }


        [HttpGet("{id}")]

        public Product Get(string id)
        {
            var messageBus = RabbitHutch.CreateBus("host=localhost");

            var response = messageBus.Request<ReturnProductList, ReturnedProductList>(new ReturnProductList
            {
                Records = 5,
                Param = id
            });

            return response.Product;
        }


    }

}