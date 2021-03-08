using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitSubscriber.DataLayer
{
    class DbConnectivitySettings
    {

        public string ProductCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
