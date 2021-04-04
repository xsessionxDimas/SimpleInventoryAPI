using SimpleInventoryAPI.Queries;
using SimpleInventoryAPI.QueryDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Services
{
    public class UserService
    {
        private readonly UserQuery query;

        public UserService(UserQuery query)
        {
            this.query = query;
        }

        public Task<UserModel> GetUserInfo(string username)
        {
            return query.GetUserInfo(username);
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return query.GetUsers();
        }
    }
}
