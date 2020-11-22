using System.Threading.Tasks;
using Domain.context.mongo;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Domain.repository.repositories.implementations
{
    public class DishRepository : MongoBaseRepository<Dish>, IDishRepository
    {
        public DishRepository(IMongoDRMDbContext context) : base(context)
        {
        }

        public async Task ChangeActiveState(string id, bool active)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<Dish>.Filter.Eq("_id", objectId);
            var update = Builders<Dish>.Update.Set("Active", active);

            await _dbCollection.UpdateOneAsync(filter, update);
        }
    }
}