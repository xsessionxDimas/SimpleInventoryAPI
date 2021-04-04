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
    public class PurchaseOrderController: ControllerBase
    {
        private readonly PurchaseOrderService service;
        private readonly IMapper mapper;

        public PurchaseOrderController(PurchaseOrderService service, IMapper mapper)
        {
            this.service = service;
            this.mapper  = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddPurchaseOrder([FromBody] PurchaseOrderModel model)
        {
            try
            {
                var purchaseOrder = mapper.Map<PurchaseOrder>(model);
                var items         = mapper.Map<List<PurchaseOrderItem>>(model.Items);
                foreach(var item in items)
                {
                    item.SetCreatedBy(model.User);
                }
                purchaseOrder.Items = items;
                purchaseOrder.SetCreatedBy(model.User);
                await service.AddPurchaseOrder(purchaseOrder);
                return Ok(new Response { Status = "Success", Message = "Purchase order created successfully" });

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
        public async Task<PurchaseOrder> GetPurchaseOrderById(int id)
        {
            return await service.GetPurchaseOrderById(id);
        }

        [HttpGet]
        [Route("GetList")]
        public IEnumerable<PurchaseOrder> GetPurchaseOrders()
        {
            var param = new Dictionary<string, object>();
            param.Add("IsDeleted", false);
            return service.GetPurchaseOrders(param);
        }
    }
}
