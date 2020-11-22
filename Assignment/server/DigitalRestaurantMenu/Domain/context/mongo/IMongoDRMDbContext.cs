using MongoDB.Driver;

namespace Domain.context.mongo
{
    public interface IMongoDRMDbContext
    {
        IMongoCollection<Dish> GetCollection<Dish>(string name);
    }
}