using Microsoft.EntityFrameworkCore;
using SimpleInventoryAPI.DBContext;
using SimpleInventoryAPI.QueryDTOs;
using System.Collections.Generic;
using System.Linq;

namespace SimpleInventoryAPI.Queries
{
    public class SelectTwoQuery
    {
        private readonly SimpleInventoryDbContext dbContext;

        public SelectTwoQuery(SimpleInventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<SelectTwoModel> GetSupplierDropdownDataSource()
        {
            var query = "select Cast(Id as CHAR) as id, SupplierName as text from suppliers where IsDeleted = false";
            return dbContext.SelectTwoModels.FromSqlRaw(query).ToList();
        }

        public IEnumerable<SelectTwoModel> GetProductDropdownDataSource()
        {
            var query = "select Cast(Id as CHAR) as id, ProductName as text from products where IsDeleted = false";
            return dbContext.SelectTwoModels.FromSqlRaw(query).ToList();
        }

        public IEnumerable<SelectTwoModel> GetComponentDropdownDataSource()
        {
            var query = "select Cast(Id as CHAR) as id, PartNumber as text from components where IsDeleted = false";
            return dbContext.SelectTwoModels.FromSqlRaw(query).ToList();
        }

        public IEnumerable<SelectTwoModel> GetRolesDropdownDataSource()
        {
            var query = "select Id as id, Name as text from aspnetroles";
            return dbContext.SelectTwoModels.FromSqlRaw(query).ToList();
        }
    }
}
