using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Services
{
    public class ProductComponentService
    {
        private readonly IRepository<ProductComponent, int> repository;

        public ProductComponentService(IRepository<ProductComponent, int> repository)
        {
            this.repository = repository;
        }

        /* crud */
        public Task AddProductComponent(ProductComponent productComponent)
        {
            return repository.Add(productComponent);
        }

        public Task UpdateProductComponent(ProductComponent productComponent)
        {
            return repository.Update(productComponent);
        }

        public Task DeleteProductComponent(ProductComponent productComponent)
        {
            return repository.Delete(productComponent);
        }

        /* queries */
        public ValueTask<ProductComponent> GetProductComponentById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<ProductComponent> GetProductComponents(IDictionary<string, object> param)
        {
            return repository.GetListByParam(param);
        }
    }
}
