using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.DBContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Repositories
{
    public class ComponentRepository : IRepository<Component, int>
    {
        private readonly SimpleInventoryDbContext dbContext;

        public ComponentRepository(SimpleInventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task Add(Component item)
        {
            dbContext.Add(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Update(Component item)
        {
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public Task Delete(Component item)
        {
            item.IsDeleted = true;
            dbContext.Update(item);
            return dbContext.SaveChangesAsync();
        }

        public ValueTask<Component> GetById(int key)
        {
            var param = new object[]{ key };
            return dbContext.Components.FindAsync(param);
        }

        public IEnumerable<Component> GetListByParam(IDictionary<string, object> param)
        {
            var rawQuery      = "select * from components ";
            var sqlParameters = new List<MySqlParameter>();
            var keys          = param.Keys.ToArray();
            for (var i = 0; i < keys.Length; i++)
            {
                rawQuery      += i == 0 ? "where " + keys[i] + " = {" + i + "}" :keys[i] + " = {" + i + "}";
                var paramNum   = string.Format("@p{0}", i);
                var paramValue = param[keys[i]];
                sqlParameters.Add(new MySqlParameter(paramNum, paramValue));
            }
            return  dbContext.Components.FromSqlRaw(rawQuery, sqlParameters.ToArray()).ToList();
        }
    }
}
