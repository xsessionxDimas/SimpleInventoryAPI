using SimpleInventoryAPI.Queries;
using SimpleInventoryAPI.QueryDTOs;
using System.Collections.Generic;

namespace SimpleInventoryAPI.Services
{
    public class RoleService
    {
        private readonly SelectTwoQuery query;

        public RoleService(SelectTwoQuery query)
        {
            this.query = query;
        }

        public IEnumerable<SelectTwoModel> GetRolesDropdownDataSource()
        {
            return query.GetRolesDropdownDataSource();
        }
    }
}
