using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SimpleInventoryAPI.Services
{
    public class ProductBatchService
    {
        private readonly IRepository<ProductBatch, int> repository;

        public ProductBatchService(IRepository<ProductBatch, int> repository)
        {
            this.repository = repository;
        }

        /* crud */
        public Task AddProductBatch(ProductBatch productBatch)
        {
            return repository.Add(productBatch);
        }

        public Task UpdateProductBatch(ProductBatch productBatch)
        {
            return repository.Update(productBatch);
        }

        public Task DeleteProductBatch(ProductBatch productBatch)
        {
            return repository.Delete(productBatch);
        }

        /* queries */
        public Task<ProductBatch> GetProductBatchById(int id)
        {
            return ((ProductBatchRepository)repository).GetById(id);
        }

        public IEnumerable<ProductBatch> GetProductBatchs(IDictionary<string, object> param)
        {
            return repository.GetListByParam(param);
        }
    }
}
