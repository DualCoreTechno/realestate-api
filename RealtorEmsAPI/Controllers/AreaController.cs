using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AreaController : BaseController
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet("get-all-areas")]
        public async Task<ActionResult> GetAllareas()
        {
            ResponseBaseModel result = await _areaService.GetAllAreas();
            return Ok(result);
        }
        
        [HttpPost("add-update-area")]
        public async Task<ActionResult> AddOrUpdateArea(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _areaService.AddOrUpdateArea(request);
            return Ok(result);
        }

        [HttpDelete("delete-area/{id}")]
        public async Task<ActionResult> DeleteArea(int id)
        {
            ResponseBaseModel result = await _areaService.DeleteArea(id);
            return Ok(result);
        }
    }
}
