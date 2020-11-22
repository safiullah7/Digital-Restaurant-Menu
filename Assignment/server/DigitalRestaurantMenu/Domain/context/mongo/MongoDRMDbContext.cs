using MongoDB.Driver;

namespace Domain.context.mongo
{
    public class MongoDRMDbContext: IMongoDRMDbContext
    {
        private IMongoDatabase _db {get; set;}
        private MongoClient _mongoClient {get; set;}
        public IClientSessionHandle Session { get; set; }
        public MongoDRMDbContext(string connection, string databaseName)
        {
            _mongoClient = new MongoClient(connection);
            _db = _mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}