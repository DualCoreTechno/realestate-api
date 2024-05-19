using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityController : BaseController
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet("get-all-activities")]
        public async Task<ActionResult> GetAllActivies()
        {
            ResponseBaseModel result = await _activityService.GetAllActivity();
            return Ok(result);
        }

        [HttpGet("get-all-parent-activities")]
        public async Task<ActionResult> GetAllParentActivies()
        {
            ResponseBaseModel result = await _activityService.GetAllParentActivity();
            return Ok(result);
        }

        [HttpPost("add-update-activity")]
        public async Task<ActionResult> AddOrUpdateActivity(ActivityModel request)
        {
            ResponseBaseModel result = await _activityService.AddOrUpdateActivity(request);
            return Ok(result);
        }

        [HttpDelete("delete-activity/{id}")]
        public async Task<ActionResult> DeleteActivity(int id)
        {
            ResponseBaseModel result = await _activityService.DeleteActivity(id);
            return Ok(result);
        }
    }
}
