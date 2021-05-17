using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.DBContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleInventoryAPI.Repositories
{
    public class CurrencyRateRepository : IRepository<CurrencyRate, int>
    {
        private readonly SimpleInventoryDbContext dbContext;

        public CurrencyRateRepository(SimpleInventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task Add(CurrencyRate item)
        {
            dbContext.Add(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Update(CurrencyRate item)
        {
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Delete(CurrencyRate item)
        {
            item.IsDeleted = true;
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public ValueTask<CurrencyRate> GetById(int key)
        {
            var param = new object[]{ key };
            return dbContext.CurrencyRates.FindAsync(param);
        }

        public IEnumerable<CurrencyRate> GetListByParam(IDictionary<string, object> param)
        {
            var rawQuery      = "select * from currencyrates ";
            var sqlParameters = new List<MySqlParameter>();
            var keys          = param.Keys.ToArray();
            for (var i = 0; i < keys.Length; i++)
            {
                rawQuery      += i == 0 ? "where " + keys[i] + " = {" + i + "}" :keys[i] + " = {" + i + "}";
                var paramNum   = string.Format("@p{0}", i);
                var paramValue = param[keys[i]];
                sqlParameters.Add(new MySqlParameter(paramNum, paramValue));
            }
            return  dbContext.CurrencyRates.FromSqlRaw(rawQuery, sqlParameters.ToArray()).ToList();
        }
    }
}
