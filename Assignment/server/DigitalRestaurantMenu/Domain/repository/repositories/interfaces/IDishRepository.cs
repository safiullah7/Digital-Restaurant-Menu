using System.Threading.Tasks;
using Domain.models;

namespace Domain.repository.repositories.interfaces
{
    public interface IDishRepository: IBaseRepository<Dish>
    {
        Task ChangeActiveState(string id, bool active);
    }
}