using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Order.Messages.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Product_Id { get; set; }
        public string ProductName { get; set; }
        public DateTime EntryDate { get; set; }
        public double Price { get; set; }
    }
}
