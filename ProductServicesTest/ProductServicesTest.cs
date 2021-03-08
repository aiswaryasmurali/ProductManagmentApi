using System;
using Xunit;
using RabbitSubscriber.Services;
using Order.Messages;
using System.Collections.Generic;

namespace ProductServicesTest
{
    public class ProductServicesTest
    {
        RabbitSubscriber.Services.ProductServices productServices;
        List<Product> product = new List<Product>();
        [Fact]
        public void GetProduct()
        {
            Assert.NotNull(productServices.Get());

        }
        [Fact]
        public void GetProductDetails()
        {

            Assert.NotNull(productServices.Get("_id6044f728a41aa4653838ede6"));

        }
    }
}
