using System;
using System.Collections.Generic;
using System.Text;
using Order.Messages.Models;
using MongoDB.Driver;

namespace RabbitSubscriber.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;
        ProductDatabaseSettings _databaseSettings;
        public ProductService()
        {
            SetDbConnection();
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _products = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);

        }


        public void SetDbConnection()
        {
            _databaseSettings = new ProductDatabaseSettings();
            _databaseSettings.ConnectionString = "mongodb://localhost:27017";
            _databaseSettings.DatabaseName = "ProductMangement";
            _databaseSettings.ProductCollectionName = "Product";

        }
        public List<Product> Get()
        {
            List<Product> products;
            products = _products.Find(prod => true).ToList();

            return products;
        }



        public Product Get(string id) =>
            _products.Find<Product>(prod => prod.Id == id).FirstOrDefault();
    }
}
