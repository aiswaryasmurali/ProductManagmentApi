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
        private readonly IMongoCollection<PriceReduction> _reductions;
        ProductDatabaseSettings _databaseSettings;
        public ProductService()
        {
            SetDbConnection();
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _products = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _reductions = database.GetCollection<PriceReduction>(_databaseSettings.ReductionCollectionName);

        }


        public void SetDbConnection()
        {
            _databaseSettings = new ProductDatabaseSettings();
            _databaseSettings.ConnectionString = "mongodb://localhost:27017";
            _databaseSettings.DatabaseName = "ProductMangement";
            _databaseSettings.ProductCollectionName = "Product";
            _databaseSettings.ReductionCollectionName = "PriceReductions";

        }
        public List<Product> Get()
        {
            List<Product> products;
            products = _products.Find(prod => true).ToList();

            return products;
        }



        public Product Get(string id)
        {
            Product currentproduct = new Product();

            PriceReduction currentreduction = new PriceReduction();

            
            currentproduct =_products.Find<Product>(prod => prod.Id == id).FirstOrDefault();

            int currentday = getDay();
            if (currentday != -1)
            {
                currentreduction = _reductions.Find<PriceReduction>(reduction => reduction.DayOfWeek == currentday).FirstOrDefault();
                currentproduct.Price = currentproduct.Price - currentreduction.Reduction;
            }


            
            return currentproduct;
        }

        enum Days
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday

        }


        public int getDay()
        {
            DayOfWeek currentday = DateTime.Today.DayOfWeek;
            switch ((Days)currentday)
            {
                case Days.Sunday:
                    return 0;
                    break;
                case Days.Monday:
                    return 1;
                    break;
                case Days.Tuesday:
                    return 2;
                    break;
                case Days.Wednesday:
                    return 3;
                    break;
                case Days.Thursday:
                    return 4;
                    break;
                case Days.Friday:
                    return 5;
                    break;
                case Days.Saturday:
                    return 6;
                    break;

                default:
                    return -1;
            }

        }
    }
}
