using System.Collections.Generic;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Repositories;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Services
{
    public class PurchaseOrderService
    {
        private readonly IRepository<PurchaseOrder, int> repository;

        public PurchaseOrderService(IRepository<PurchaseOrder, int> repository)
        {
            this.repository = repository;
        }

        /* crud */
        public Task AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            return repository.Add(purchaseOrder);
        }

        public Task UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            return repository.Update(purchaseOrder);
        }

        public void ApplyPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            ((PurchaseOrderRepository)repository).Apply(purchaseOrder);
        }

        public Task DeletePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            return repository.Delete(purchaseOrder);
        }

        /* queries */
        public Task<PurchaseOrder> GetPurchaseOrderById(int id)
        {
            return ((PurchaseOrderRepository)repository).GetById(id);
        }

        public IEnumerable<PurchaseOrder> GetPurchaseOrders(IDictionary<string, object> param)
        {
            return repository.GetListByParam(param);
        }
    }
}
