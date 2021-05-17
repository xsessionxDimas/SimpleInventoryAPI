using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryAPI.Models;
using SimpleInventoryAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SimpleInventoryAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyRateService service;
        private readonly IMapper mapper;

        public CurrencyController(CurrencyRateService service, IMapper mapper)
        {
            this.service = service;
            this.mapper  = mapper;
        }

        [HttpGet]
        [Route("GetList")]
        public IEnumerable<CurrencyRateModel> GetSuppliers()
        {
            var param = new Dictionary<string, object>();
            param.Add("IsDeleted", false);
            return mapper.Map<IEnumerable<CurrencyRateModel>>(service.GetCurrencyRates(param));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateCurrencyRate([FromBody] CurrencyRateModel model)
        {
            try
            {
                var currencyRate  = await service.GetCurrencyRateById(model.Id);
                currencyRate.Rate = model.Rate;
                currencyRate.SetModifyByAndModifyDate(model.User);
                await service.UpdateCurrencyRate(currencyRate);
                return Ok(new Response { Status = "Success", Message = "Rate updated successfully" });

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
    }
}
