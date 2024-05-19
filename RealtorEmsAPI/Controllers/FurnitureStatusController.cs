using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FurnitureStatusController : BaseController
    {
        private readonly IFurnitureStatusService _furnitureStatusService;

        public FurnitureStatusController(IFurnitureStatusService furnitureStatusService)
        {
            _furnitureStatusService = furnitureStatusService;
        }

        [HttpGet("get-all-furniture-status")]
        public async Task<ActionResult> GetAllFurnitureStatus()
        {
            ResponseBaseModel result = await _furnitureStatusService.GetAllFurnitureStatus();
            return Ok(result);
        }

        [HttpPost("add-update-furniture-status")]
        public async Task<ActionResult> AddOrUpdateFurnitureStatus(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _furnitureStatusService.AddOrUpdateFurnitureStatus(request);
            return Ok(result);
        }

        [HttpDelete("delete-furniture-status/{id}")]
        public async Task<ActionResult> DeleteFurnitureStatus(int id)
        {
            ResponseBaseModel result = await _furnitureStatusService.DeleteFurnitureStatus(id);
            return Ok(result);
        }
    }
}
