using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.DBContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Repositories
{
    public class ProductRepository: IRepository<Product, int>
    {
        private readonly SimpleInventoryDbContext dbContext;

        public ProductRepository(SimpleInventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task Add(Product item)
        {
            dbContext.Add(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Update(Product item)
        {
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Delete(Product item)
        {
            item.IsDeleted = true;
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public ValueTask<Product> GetById(int key)
        {
            var param = new object[]{ key };
            return dbContext.Products.FindAsync(param);
        }

        public IEnumerable<Product> GetListByParam(IDictionary<string, object> param)
        {
            var rawQuery      = "select * from products ";
            var sqlParameters = new List<MySqlParameter>();
            var keys          = param.Keys.ToArray();
            for (var i = 0; i < keys.Length; i++)
            {
                rawQuery      += i == 0 ? "where " + keys[i] + " = {" + i + "}" :keys[i] + " = {" + i + "}";
                var paramNum   = string.Format("@p{0}", i);
                var paramValue = param[keys[i]];
                sqlParameters.Add(new MySqlParameter(paramNum, paramValue));
            }
            return  dbContext.Products.FromSqlRaw(rawQuery, sqlParameters.ToArray()).ToList();
        }
    }
}
