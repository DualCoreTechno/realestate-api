using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PriceController : BaseController
    {
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        [HttpGet("get-all-price")]
        public async Task<ActionResult> GetAllPrice()
        {
            ResponseBaseModel result = await _priceService.GetAllPrice();
            return Ok(result);
        }

        [HttpPost("add-update-price")]
        public async Task<ActionResult> AddOrUpdatePrice(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _priceService.AddOrUpdatePrice(request);
            return Ok(result);
        }

        [HttpDelete("delete-price/{id}")]
        public async Task<ActionResult> DeletePrice(int id)
        {
            ResponseBaseModel result = await _priceService.DeletePrice(id);
            return Ok(result);
        }
    }
}
