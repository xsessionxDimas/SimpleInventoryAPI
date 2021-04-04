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
    public class ProductController : ControllerBase
    {
        private readonly ProductService service;
        private readonly IMapper mapper;

        public ProductController(ProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper  = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel model)
        {
            try
            {
                var Product = mapper.Map<Product>(model);
                Product.SetCreatedBy(model.User);
                await service.AddProduct(Product);
                return Ok(new Response { Status = "Success", Message = "Product created successfully" });

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
        public async Task<IActionResult> UpdateProduct([FromBody] ProductModel model)
        {
            try
            {
                var product         = await service.GetProductById(model.Id);
                product.Description = model.Description;
                product.VAT         = model.VAT;
                product.SalesFee    = model.SalesFee;
                product.SetModifyByAndModifyDate(model.User);
                await service.UpdateProduct(product);
                return Ok(new Response { Status = "Success", Message = "Product updated successfully" });

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
        public async Task<IActionResult> DeleteProduct([FromBody] ProductModel model)
        {
            try
            {
                var Product = await service.GetProductById(model.Id);
                Product.SetModifyByAndModifyDate(model.User);
                await service.DeleteProduct(Product);
                return Ok(new Response { Status = "Success", Message = "Product deleted successfully" });

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
        public IEnumerable<Product> GetProducts()
        {
            var param = new Dictionary<string, object>();
            param.Add("IsDeleted", false);
            return service.GetProducts(param);
        }

        [HttpGet]
        [Route("GetAsDataSource")]
        public IEnumerable<SelectTwoModel> GetProductDropdownDataSource()
        {
            return service.GetProductDropdownDataSource();
        }
    }
}
