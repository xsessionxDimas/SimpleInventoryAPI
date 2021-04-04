using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryAPI.QueryDTOs;
using SimpleInventoryAPI.Services;
using System.Collections.Generic;

namespace SimpleInventoryAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService service;

        public RoleController(RoleService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("GetAsDataSource")]
        public IEnumerable<SelectTwoModel> GetRolesDropdownDataSource()
        {
             return service.GetRolesDropdownDataSource();
        }
    }
}
