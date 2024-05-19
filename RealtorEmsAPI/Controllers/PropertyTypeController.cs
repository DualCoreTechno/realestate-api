using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyTypeController : BaseController
    {
        private readonly IPropertyTypeService _propertyTypeService;

        public PropertyTypeController(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }

        [HttpGet("get-all-property-types")]
        public async Task<ActionResult> GetAllPropertyType()
        {
            ResponseBaseModel result = await _propertyTypeService.GetAllPropertyType();
            return Ok(result);
        }

        [HttpPost("add-update-property-type")]
        public async Task<ActionResult> AddOrUpdatePropertyType(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _propertyTypeService.AddOrUpdatePropertyType(request);
            return Ok(result);
        }

        [HttpDelete("delete-property-type/{id}")]
        public async Task<ActionResult> DeletePropertyType(int id)
        {
            ResponseBaseModel result = await _propertyTypeService.DeletePropertyType(id);
            return Ok(result);
        }
    }
}