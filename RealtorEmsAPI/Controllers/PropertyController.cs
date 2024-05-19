using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : BaseController
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet("get-pageload-data")]
        public async Task<ActionResult> GetPageLoadData()
        {
            ResponseBaseModel result = await _propertyService.GetPageLoadDataAsync();
            return Ok(result);
        }

        [HttpPost("get-all-property")]
        public async Task<ActionResult> GetAllProperty(PropertyRequestModel propertyRequestModel)
        {
            ResponseBaseModel result = await _propertyService.GetAllPropertyAsync(propertyRequestModel, CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }

        [HttpPost("add-update-property")]
        public async Task<ActionResult> AddOrUpdateProperty(PropertyModel request)
        {
            request.UserId = CurrentUser.UserId;

            ResponseBaseModel result = await _propertyService.AddOrUpdatePropertyAsync(request);
            return Ok(result);
        }

        [HttpDelete("delete-property/{id}")]
        public async Task<ActionResult> DeleteProperty(int id)
        {
            ResponseBaseModel result = await _propertyService.DeletePropertyAsync(id);
            return Ok(result);
        }
    }
}
