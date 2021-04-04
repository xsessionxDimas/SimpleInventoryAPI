using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Queries;
using SimpleInventoryAPI.QueryDTOs;
using SimpleInventoryAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Services
{
    public class ProductService
    {
        private readonly IRepository<Product, int> repository;
        private readonly SelectTwoQuery query;

        public ProductService(IRepository<Product, int> repository, SelectTwoQuery query)
        {
            this.repository = repository;
            this.query = query;
        }

        /* crud */
        public Task AddProduct(Product product)
        {
            return repository.Add(product);
        }

        public Task UpdateProduct(Product product)
        {
            return repository.Update(product);
        }

        public Task DeleteProduct(Product product)
        {
            return repository.Delete(product);
        }

        /* queries */
        public ValueTask<Product> GetProductById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<Product> GetProducts(IDictionary<string, object> param)
        {
            return repository.GetListByParam(param);
        }

        public IEnumerable<SelectTwoModel> GetProductDropdownDataSource()
        {
            return query.GetProductDropdownDataSource();
        }
    }
}
