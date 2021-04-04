using SimpleInventoryAPI.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Repositories
{
    public interface IRepository<T, K> where T : BaseEntity<K>
    {
        Task Add(T item);
        Task Update(T item);
        Task Delete(T item);
        ValueTask<T> GetById(K key);
        IEnumerable<T> GetListByParam(IDictionary<string, object> param);
    }
}
