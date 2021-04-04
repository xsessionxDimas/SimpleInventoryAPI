using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.DBContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Repositories
{
    public class ProductBatchRepository : IRepository<ProductBatch, int>
    {
        private readonly SimpleInventoryDbContext dbContext;

        public ProductBatchRepository(SimpleInventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task Add(ProductBatch item)
        {
            dbContext.Add(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Update(ProductBatch item)
        {
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Delete(ProductBatch item)
        {
            item.IsDeleted = true;
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public Task<ProductBatch> GetById(int key)
        {
            var param = new object[]{ key };
            return dbContext.ProductBatches.Include(p => p.Product)
                .ThenInclude(p => p.ProductComponents)
                .SingleOrDefaultAsync(p => p.Id == key);
        }

        public IEnumerable<ProductBatch> GetListByParam(IDictionary<string, object> param)
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
            return  dbContext.ProductBatches.FromSqlRaw(rawQuery, sqlParameters.ToArray()).ToList();
        }

        ValueTask<ProductBatch> IRepository<ProductBatch, int>.GetById(int key)
        {
            throw new System.NotImplementedException();
        }
    }
}
