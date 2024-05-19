using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuildingsController : BaseController
    {
        private readonly IBuildingService _buildingsService;

        public BuildingsController(IBuildingService buildingsService)
        {
            _buildingsService = buildingsService;
        }

        [HttpGet("get-all-buildings")]
        public async Task<ActionResult> GetAllBuildings()
        {
            ResponseBaseModel result = await _buildingsService.GetAllBuildings();
            return Ok(result);
        }

        [HttpPost("add-update-building")]
        public async Task<ActionResult> AddOrUpdateBuilding(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _buildingsService.AddOrUpdateBuilding(request);
            return Ok(result);  
        }

        [HttpDelete("delete-building/{id}")]
        public async Task<ActionResult> DeleteBuilding(int id)
        {
            ResponseBaseModel result = await _buildingsService.DeleteBuilding(id);
            return Ok(result);
        }
    }
}
