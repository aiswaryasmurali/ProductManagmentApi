using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Messages.Models
{
    public class ProductDatabaseSettings : IProductDatabaseSettings
    {
        public string ProductCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    
}
