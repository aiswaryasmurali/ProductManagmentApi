namespace Order.Messages.Models
{
    public interface IProductDatabaseSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
        string ProductCollectionName { get; }
    }
}