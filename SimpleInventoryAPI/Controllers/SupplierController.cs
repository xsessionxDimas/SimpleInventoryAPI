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
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService service;
        private readonly IMapper mapper;

        public SupplierController(SupplierService service, IMapper mapper)
        {
            this.service = service;
            this.mapper  = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddSupplier([FromBody] SupplierModel model)
        {
            try
            {
                var Supplier = mapper.Map<Supplier>(model);
                Supplier.SetCreatedBy(model.User);
                await service.AddSupplier(Supplier);
                return Ok(new Response { Status = "Success", Message = "Supplier created successfully" });

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
        public async Task<IActionResult> UpdateSupplier([FromBody] SupplierModel model)
        {
            try
            {
                var Supplier           = await service.GetSupplierById(model.Id);
                Supplier.SupplierName  = model.SupplierName;
                Supplier.Address       = model.Address;
                Supplier.ContactPerson = model.ContactPerson;
                Supplier.Phone         = model.Phone;
                Supplier.SetModifyByAndModifyDate(model.User);
                await service.UpdateSupplier(Supplier);
                return Ok(new Response { Status = "Success", Message = "Supplier updated successfully" });

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
        public async Task<IActionResult> DeleteSupplier([FromBody] SupplierModel model)
        {
            try
            {
                var Supplier = await service.GetSupplierById(model.Id);
                Supplier.SetModifyByAndModifyDate(model.User);
                await service.DeleteSupplier(Supplier);
                return Ok(new Response { Status = "Success", Message = "Supplier deleted successfully" });

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
        public IEnumerable<SupplierModel> GetSuppliers()
        {
            var param = new Dictionary<string, object>();
            param.Add("IsDeleted", false);
            return mapper.Map<IEnumerable<SupplierModel>>(service.GetSuppliers(param));
        }

        [HttpGet]
        [Route("GetAsDataSource")]
        public IEnumerable<SelectTwoModel> GetSuppliersDropdownDataSource()
        {
             return service.GetSuppliersDropdownDataSource();
        }
    }
}
