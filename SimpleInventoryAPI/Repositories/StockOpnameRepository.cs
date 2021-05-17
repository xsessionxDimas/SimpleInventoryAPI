using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.DBContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleInventoryAPI.Repositories
{
    public class StockOpnameRepository : IRepository<StockOpname, int>
    {
        private readonly SimpleInventoryDbContext dbContext;

        public StockOpnameRepository(SimpleInventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task Add(StockOpname item)
        {
            dbContext.Add(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Update(StockOpname item)
        {
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Delete(StockOpname item)
        {
            item.IsDeleted = true;
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public ValueTask<StockOpname> GetById(int key)
        {
            var param = new object[]{ key };
            return dbContext.StockOpnames.FindAsync(param);
        }

        public IEnumerable<StockOpname> GetListByParam(IDictionary<string, object> param)
        {
            var rawQuery      = "select * from stockopnames ";
            var sqlParameters = new List<MySqlParameter>();
            var keys          = param.Keys.ToArray();
            for (var i = 0; i < keys.Length; i++)
            {
                rawQuery      += i == 0 ? "where " + keys[i] + " = {" + i + "}" :keys[i] + " = {" + i + "}";
                var paramNum   = string.Format("@p{0}", i);
                var paramValue = param[keys[i]];
                sqlParameters.Add(new MySqlParameter(paramNum, paramValue));
            }
            return  dbContext.StockOpnames.FromSqlRaw(rawQuery, sqlParameters.ToArray()).ToList();
        }
    }
}
