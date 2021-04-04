using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryAPI.QueryDTOs;
using SimpleInventoryAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService service;

        public UserController(UserService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("GetList")]
        public IEnumerable<UserModel> GetUsers()
        {
            return service.GetUsers();
        }

        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<UserModel> GetUsers(string username)
        {
            return await service.GetUserInfo(username);
        }
    }
}
