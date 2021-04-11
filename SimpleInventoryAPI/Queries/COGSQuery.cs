using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SimpleInventoryAPI.DBContext;
using SimpleInventoryAPI.QueryDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Queries
{
    public class COGSQuery
    {
        private readonly SimpleInventoryDbContext dbContext;

        public COGSQuery(SimpleInventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<COGSModel>> GetCOGS()
        {
            var query = @"SELECT a.Id, b.ProductName, a.Type
                          FROM productcomponents a
                          JOIN products          b on b.Id = a.ProductId";
            return dbContext.COGS.FromSqlRaw(query).ToListAsync();
        }

        public Task<List<COGSItemModel>> GetCOGSItemsByHeaderId(int headerId)
        {
            var query = @"SELECT b.PartNumber as Component, a.Usage, a.CostPerUnit, a.FreightPerUnit, a.Total, 
                          a.Notes
                          FROM productcomponentitem a
                          JOIN components           b on b.Id = a.ComponentId
                          WHERE a.HeaderId = {0}";
            var sqlParameters = new List<MySqlParameter>
            {
                new MySqlParameter("@p0", headerId)
            };
            return dbContext.COGSItems.FromSqlRaw(query, sqlParameters.ToArray()).ToListAsync();
        }
    }
}
