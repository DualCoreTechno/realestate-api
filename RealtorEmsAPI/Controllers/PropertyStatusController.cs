using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyStatusController : BaseController
    {
        private readonly IPropertyStatusService _propertyStatusService;

        public PropertyStatusController(IPropertyStatusService propertyStatusService)
        {
            _propertyStatusService = propertyStatusService;
        }

        [HttpGet("get-all-property-status")]
        public async Task<ActionResult> GetAllPropertyStatus()
        {
            ResponseBaseModel result = await _propertyStatusService.GetAllPropertyStatus();
            return Ok(result);
        }

        [HttpPost("add-update-property-status")]
        public async Task<ActionResult> AddOrUpdatePropertyStatus(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _propertyStatusService.AddOrUpdatePropertyStatus(request);
            return Ok(result);
        }

        [HttpDelete("delete-property-status/{id}")]
        public async Task<ActionResult> DeletePropertyStatus(int id)
        {
            ResponseBaseModel result = await _propertyStatusService.DeletePropertyStatus(id);
            return Ok(result);
        }
    }
}
