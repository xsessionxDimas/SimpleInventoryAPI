using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Models;
using SimpleInventoryAPI.Queries;
using SimpleInventoryAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SimpleInventoryAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductComponentController : ControllerBase
    {
        private readonly ProductComponentService service;
        private readonly COGSQuery cogsQuery;
        private readonly IMapper mapper;

        public ProductComponentController(ProductComponentService service, COGSQuery cogsQuery, IMapper mapper)
        {
            this.service = service;
            this.cogsQuery = cogsQuery;
            this.mapper  = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddProductComponent([FromBody] ProductComponentModel model)
        {
            try
            {
                var component = mapper.Map<ProductComponent>(model);
                component.SetCreatedBy(model.User);
                var items     = mapper.Map<List<ProductComponentItem>>(model.Items);
                foreach(var item in items)
                {
                    item.SetCreatedBy(model.User);
                }
                component.Items = items;
                await service.AddProductComponent(component); 
                return Ok(new Response { Status = "Success", Message = "Product Component created successfully" });

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
        public async Task<IActionResult> UpdateProductComponent([FromBody] ProductComponentModel model)
        {
            try
            {
                var productComponent   = await service.GetProductComponentById(model.Id);
                productComponent.Type  = model.Type;
                productComponent.Items = mapper.Map<List<ProductComponentItem>>(model.Items);
                productComponent.SetModifyByAndModifyDate(model.User);
                await service.UpdateProductComponent(productComponent);
                return Ok(new Response { Status = "Success", Message = "Product Component updated successfully" });

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
        public async Task<IActionResult> DeleteProductComponent([FromBody] ProductComponentModel model)
        {
            try
            {
                var component = await service.GetProductComponentById(model.Id);
                component.SetModifyByAndModifyDate(model.User);
                await service.DeleteProductComponent(component);
                return Ok(new Response { Status = "Success", Message = "Product Component deleted successfully" });

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
        public async Task<List<QueryDTOs.COGSModel>> GetCOGS()
        {
            return await cogsQuery.GetCOGS().ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetListById")]
        public async Task<List<QueryDTOs.COGSItemModel>> GetCOGSComponents([FromQuery]int id, string query)
        {
            return await cogsQuery.GetCOGSItemsByHeaderId(id).ConfigureAwait(false);
        }
    }
}
