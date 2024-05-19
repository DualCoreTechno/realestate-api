using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyDealController : BaseController
    {
        private readonly IPropertyDealService _propertyDealService;

        public PropertyDealController(IPropertyDealService propertyDealService)
        {
            _propertyDealService = propertyDealService;
        }

        [HttpGet("get-pageload-data")]
        public async Task<ActionResult> GetPageLoadData()
        {
            ResponseBaseModel result = await _propertyDealService.GetPageLoadDataAsync(CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }

        [HttpGet("get-all-propertydeal")]
        public async Task<ActionResult> GetAllPropertyDeal()
        {
            ResponseBaseModel result = await _propertyDealService.GetAllPropertyDealAsync(CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }

        [HttpPost("add-update-propertydeal")]
        public async Task<ActionResult> AddOrUpdatePropertyDeal(PropertyDealModel request)
        {
            ResponseBaseModel result = await _propertyDealService.AddOrUpdatePropertyDealAsync(request);
            return Ok(result);
        }

        [HttpDelete("delete-propertydeal/{id}")]
        public async Task<ActionResult> DeletePropertyDeal(int id)
        {
            ResponseBaseModel result = await _propertyDealService.DeletePropertyDealAsync(id);
            return Ok(result);
        }
    }
}
