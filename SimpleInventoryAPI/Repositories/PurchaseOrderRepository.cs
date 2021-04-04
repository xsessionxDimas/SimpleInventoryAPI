using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.DBContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleInventoryAPI.Repositories
{
    public class PurchaseOrderRepository: IRepository<PurchaseOrder, int>
    {
        private readonly SimpleInventoryDbContext dbContext;

        public PurchaseOrderRepository(SimpleInventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task Add(PurchaseOrder item)
        {
            dbContext.Add(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Update(PurchaseOrder item)
        {
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Delete(PurchaseOrder item)
        {
            item.IsDeleted = true;
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public Task<PurchaseOrder> GetById(int key)
        {
            var param = new object[]{ key };
            return dbContext.PurchaseOrders.Include(p => p.Items)
                .ThenInclude(p => p.Component)
                .SingleOrDefaultAsync(p => p.Id == key);
        }

        public IEnumerable<PurchaseOrder> GetListByParam(IDictionary<string, object> param)
        {
            var rawQuery      = "select * from productbatches ";
            var sqlParameters = new List<MySqlParameter>();
            var keys          = param.Keys.ToArray();
            for (var i = 0; i < keys.Length; i++)
            {
                rawQuery      += i == 0 ? "where " + keys[i] + " = {" + i + "}" :keys[i] + " = {" + i + "}";
                var paramNum   = string.Format("@p{0}", i);
                var paramValue = param[keys[i]];
                sqlParameters.Add(new MySqlParameter(paramNum, paramValue));
            }
            return  dbContext.PurchaseOrders.FromSqlRaw(rawQuery, sqlParameters.ToArray()).ToList();
        }

        ValueTask<PurchaseOrder> IRepository<PurchaseOrder, int>.GetById(int key)
        {
            throw new System.NotImplementedException();
        }
    }
}
