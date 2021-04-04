using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Queries;
using SimpleInventoryAPI.QueryDTOs;
using SimpleInventoryAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Services
{
    public class ComponentService
    {
        private readonly IRepository<Component, int> repository;
        private readonly SelectTwoQuery query;

        public ComponentService(IRepository<Component, int> repository, SelectTwoQuery query)
        {
            this.repository = repository;
            this.query = query;
        }

        /* crud */
        public Task AddComponent(Component component)
        {
            return repository.Add(component);
        }

        public Task UpdateComponent(Component component)
        {
            return repository.Update(component);
        }

        public Task DeleteComponent(Component component)
        {
            return repository.Delete(component);
        }

        /* queries */
        public ValueTask<Component> GetComponentById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<Component> GetComponents(IDictionary<string, object> param)
        {
            return repository.GetListByParam(param);
        }

        public IEnumerable<SelectTwoModel> GetComponentDropdownDataSource()
        {
            return query.GetComponentDropdownDataSource();
        }
    }
}
