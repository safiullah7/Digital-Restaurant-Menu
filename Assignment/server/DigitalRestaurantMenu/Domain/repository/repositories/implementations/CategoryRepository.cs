using Domain.context.mongo;
using Domain.models;
using Domain.repository.repositories.interfaces;

namespace Domain.repository.repositories.implementations
{
    public class CategoryRepository : MongoBaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IMongoDRMDbContext context) : base(context)
        {
        }
    }
}