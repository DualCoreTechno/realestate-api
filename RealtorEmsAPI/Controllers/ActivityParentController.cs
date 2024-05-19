using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityParentController : BaseController
    {
        private readonly IActivityParentService _activityParentService;

        public ActivityParentController(IActivityParentService activityParentService)
        {
            _activityParentService = activityParentService;
        }

        [HttpGet("get-all-activityparent")]
        public async Task<ActionResult> GetAllActivityParent()
        {
            ResponseBaseModel result = await _activityParentService.GetAllActivityParent();
            return Ok(result);
        }
        
        [HttpPost("add-update-activityparent")]
        public async Task<ActionResult> AddOrUpdateActivityParent(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _activityParentService.AddOrUpdateActivityParent(request);
            return Ok(result);
        }

        [HttpDelete("delete-activityparent/{id}")]
        public async Task<ActionResult> DeleteActivityParent(int id)
        {
            ResponseBaseModel result = await _activityParentService.DeleteActivityParent(id);
            return Ok(result);
        }
    }
}
