using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.context.mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Domain.repository
{
    public class MongoBaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected IMongoDRMDbContext _mongoContext;
        protected IMongoCollection<T> _dbCollection;

        public MongoBaseRepository(IMongoDRMDbContext context)
        {
            _mongoContext = context;
            _dbCollection = context.GetCollection<T>(typeof(T).Name);
        }

        public async Task Create(T obj)
        {
            _dbCollection = _mongoContext.GetCollection<T>(typeof(T).Name);
            await _dbCollection.InsertOneAsync(obj);
        }

        public async Task Delete(string id)
        {
            var objectId = new ObjectId(id);
            await _dbCollection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));
        }

        public async Task<T> Get(string id)
        {
            var objectId = new ObjectId(id);
            _dbCollection = _mongoContext.GetCollection<T>(typeof(T).Name);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", objectId);
            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll()
        {
            var all = await _dbCollection.FindAsync(Builders<T>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task Update(string id, T obj)
        {
            var objectId = new ObjectId(id);
            await _dbCollection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", objectId), obj);
        }
    }
}