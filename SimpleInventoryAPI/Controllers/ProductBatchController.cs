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
    public class ProductBatchController : ControllerBase
    {
        private readonly ProductBatchService service;
        private readonly IMapper mapper;

        public ProductBatchController(ProductBatchService service, IMapper mapper)
        {
            this.service = service;
            this.mapper  = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddProductBatch([FromBody] ProductBatchModel model)
        {
            try
            {
                var ProductBatch = mapper.Map<ProductBatch>(model);
                ProductBatch.SetCreatedBy(model.User);
                await service.AddProductBatch(ProductBatch);
                return Ok(new Response { Status = "Success", Message = "Product Batch created successfully" });

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
        [Route("Detail")]
        public async Task<ProductBatch> GetProductBatchById(int id)
        {
            return await service.GetProductBatchById(id);
        }

        [HttpGet]
        [Route("GetList")]
        public IEnumerable<ProductBatch> GetProductBatchs()
        {
            var param = new Dictionary<string, object>();
            param.Add("IsDeleted", false);
            return service.GetProductBatchs(param);
        }
    }
}
