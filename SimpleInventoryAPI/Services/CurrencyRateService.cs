using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Services
{
    public class CurrencyRateService
    {
        private readonly IRepository<CurrencyRate, int> repository;

        public CurrencyRateService(IRepository<CurrencyRate, int> repository)
        {
            this.repository = repository;
        }
        
        public Task UpdateCurrencyRate(CurrencyRate productBatch)
        {
            return repository.Update(productBatch);
        }

        /* queries */
        public ValueTask<CurrencyRate> GetCurrencyRateById(int id)
        {
            return ((CurrencyRateRepository)repository).GetById(id);
        }

        public IEnumerable<CurrencyRate> GetCurrencyRates(IDictionary<string, object> param)
        {
            return repository.GetListByParam(param);
        }
    }
}
