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
    public class PurchaseOrderController: ControllerBase
    {
        private readonly PurchaseOrderService service;
        private readonly PurchaseOrderQuery poQuery;
        private readonly IMapper mapper;

        public PurchaseOrderController(PurchaseOrderService service, PurchaseOrderQuery poQuery, IMapper mapper)
        {
            this.service = service;
            this.poQuery = poQuery;
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

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdatePurchaseOrder([FromBody] PurchaseOrderModel model)
        {
            try
            {
                var purchaseOrder   = await service.GetPurchaseOrderById(model.Id);
                purchaseOrder.Items = mapper.Map<List<PurchaseOrderItem>>(model.Items);
                purchaseOrder.SetModifyByAndModifyDate(model.User);
                await service.UpdatePurchaseOrder(purchaseOrder);
                return Ok(new Response { Status = "Success", Message = "Purchase order updated successfully" });

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
        [Route("Apply")]
        public async Task<IActionResult> ApplyPurchaseOrder([FromBody] PurchaseOrderActionModel model)
        {
            try
            {
                var purchaseOrder     = await service.GetPurchaseOrderById(model.Id);
                purchaseOrder.IsDraft = false;
                purchaseOrder.SetModifyByAndModifyDate(model.User);
                service.ApplyPurchaseOrder(purchaseOrder);
                return Ok(new Response { Status = "Success", Message = "Purchase order updated successfully" });
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
        public async Task<QueryDTOs.POModel> GetPurchaseOrderById(int id)
        {
            return await poQuery.GetPurchaseOrder(id);
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<List<QueryDTOs.POModel>> GetPurchaseOrders()
        {
            return await poQuery.GetPurchaseOrders().ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetListById")]
        public async Task<List<QueryDTOs.POItemModel>> GetPurchaseOrderItems([FromQuery] int id)
        {
            return await poQuery.GetPurchaseOrderItemsByHeaderId(id).ConfigureAwait(false);
        }
    }
}
