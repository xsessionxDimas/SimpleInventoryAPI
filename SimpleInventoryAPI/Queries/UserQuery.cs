using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SimpleInventoryAPI.DBContext;
using SimpleInventoryAPI.QueryDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Queries
{
    public class UserQuery
    {
        private readonly SimpleInventoryDbContext dbContext;

        public UserQuery(SimpleInventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<UserModel> GetUserInfo(string username)
        {
            var query = @"SELECT a.UserName, a.Email, c.Name as Role 
                          FROM aspnetusers a
                          JOIN aspnetuserroles b on a.Id = b.UserId
                          JOIN aspnetroles     c on c.Id = b.RoleId
                          WHERE a.UserName = {0}";
            var sqlParameters = new List<MySqlParameter>();
            sqlParameters.Add(new MySqlParameter("@p0", username));
            return dbContext.UserModels.FromSqlRaw(query, sqlParameters.ToArray()).FirstOrDefaultAsync();
        }

        public IEnumerable<UserModel> GetUsers()
        {
            var query = @"SELECT a.UserName, a.Email, c.Name as Role 
                          FROM aspnetusers a
                          JOIN aspnetuserroles b on a.Id = b.UserId
                          JOIN aspnetroles     c on c.Id = b.RoleId";
            return dbContext.UserModels.FromSqlRaw(query);
        }
    }
}
