using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.repository
{
    public interface IBaseRepository<T> where T: class
    {
        Task Create(T obj);
        Task Update(string id, T obj);
        Task Delete(string id);
        Task<T> Get(string id);
        Task<List<T>> GetAll();
    }
}