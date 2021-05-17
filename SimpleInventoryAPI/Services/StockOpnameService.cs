using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Queries;
using SimpleInventoryAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Services
{
    public class StockOpnameService
    {
        private readonly IRepository<StockOpname, int> repository;
        private readonly SelectTwoQuery query;

        public StockOpnameService(IRepository<StockOpname, int> repository, SelectTwoQuery query)
        {
            this.repository = repository;
            this.query = query;
        }

        /* crud */
        public Task AddStockOpname(StockOpname component)
        {
            return repository.Add(component);
        }

        public Task UpdateStockOpname(StockOpname component)
        {
            return repository.Update(component);
        }

        public Task DeleteStockOpname(StockOpname component)
        {
            return repository.Delete(component);
        }

        /* queries */
        public ValueTask<StockOpname> GetStockOpnameById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<StockOpname> GetStockOpnames(IDictionary<string, object> param)
        {
            return repository.GetListByParam(param);
        }        
    }
}
