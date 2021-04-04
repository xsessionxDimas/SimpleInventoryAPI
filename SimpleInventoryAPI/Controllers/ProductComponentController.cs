using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryAPI.DataAccess;
using SimpleInventoryAPI.Models;
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
        private readonly IMapper mapper;

        public ProductComponentController(ProductComponentService service, IMapper mapper)
        {
            this.service = service;
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
                var productComponent = await service.GetProductComponentById(model.Id);
                productComponent.Usage          = model.Usage;
                productComponent.CostPerUnit    = model.CostPerUnit;
                productComponent.FreightPerUnit = model.FreightPerUnit;
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
        public IEnumerable<ProductComponent> GetProductComponents()
        {
            var param = new Dictionary<string, object>();
            param.Add("IsDeleted", false);
            return service.GetProductComponents(param);
        }
    }
}
