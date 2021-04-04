using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Queries;
using SimpleInventoryAPI.QueryDTOs;
using SimpleInventoryAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Services
{
    public class SupplierService
    {
        private readonly IRepository<Supplier, int> repository;
        private readonly SelectTwoQuery query;

        public SupplierService(IRepository<Supplier, int> repository, SelectTwoQuery query)
        {
            this.repository = repository;
            this.query = query;
        }

        /* crud */
        public Task AddSupplier(Supplier supplier)
        {
            return repository.Add(supplier);
        }

        public Task UpdateSupplier(Supplier supplier)
        {
            return repository.Update(supplier);
        }

        public Task DeleteSupplier(Supplier supplier)
        {
            return repository.Delete(supplier);
        }

        /* queries */
        public ValueTask<Supplier> GetSupplierById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<Supplier> GetSuppliers(IDictionary<string, object> param)
        {
            return repository.GetListByParam(param);
        }

        public IEnumerable<SelectTwoModel> GetSuppliersDropdownDataSource()
        {
            return query.GetSupplierDropdownDataSource();
        }
    }
}
