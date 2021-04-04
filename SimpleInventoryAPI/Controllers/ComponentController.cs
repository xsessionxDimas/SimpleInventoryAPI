using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Models;
using SimpleInventoryAPI.QueryDTOs;
using SimpleInventoryAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleInventoryAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly ComponentService service;
        private readonly IMapper mapper;

        public ComponentController(ComponentService service, IMapper mapper)
        {
            this.service = service;
            this.mapper  = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddComponent([FromBody] ComponentModel model)
        {
            try
            {
                var component = mapper.Map<Component>(model);
                component.SetCreatedBy(model.User);
                await service.AddComponent(component);
                return Ok(new Response { Status = "Success", Message = "Component created successfully" });

            }
            catch (System.Exception ex)
            {
                return BadRequest(new Response
                {
                    Status  = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateComponent([FromBody] ComponentModel model)
        {
            try
            {
                var component             = await service.GetComponentById(model.Id);
                component.PartNumber      = model.PartNumber;
                component.PartDescription = model.PartDescription;
                component.Stock           = model.Stock;
                component.SetModifyByAndModifyDate(model.User);
                await service.UpdateComponent(component);
                return Ok(new Response { Status = "Success", Message = "Component updated successfully" });

            }
            catch (System.Exception ex)
            {
                return BadRequest(new Response
                {
                    Status  = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("Delete")]
        public async Task<IActionResult> DeleteComponent([FromBody] ComponentModel model)
        {
            try
            {
                var component = await service.GetComponentById(model.Id);
                component.SetModifyByAndModifyDate(model.User);
                await service.DeleteComponent(component);
                return Ok(new Response { Status = "Success", Message = "Component deleted successfully" });

            }
            catch (System.Exception ex)
            {
                return BadRequest(new Response
                {
                    Status  = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetList")]
        public IEnumerable<Component> GetComponents()
        {
            var param = new Dictionary<string, object>();
            param.Add("IsDeleted", false);
            return service.GetComponents(param);
        }

        [HttpGet]
        [Route("GetAsDataSource")]
        public IEnumerable<SelectTwoModel> GetProductDropdownDataSource()
        {
            return service.GetComponentDropdownDataSource();
        }
    }
}
