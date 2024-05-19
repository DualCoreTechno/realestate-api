using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityChildController : BaseController
    {
        private readonly IActivityChildService _activityChildService;

        public ActivityChildController(IActivityChildService activityChildService)
        {
            _activityChildService = activityChildService;
        }

        [HttpGet("get-pageload-data")]
        public async Task<ActionResult> GetPageLoadData()
        {
            ResponseBaseModel result = await _activityChildService.GetPageLoadData();
            return Ok(result);
        }

        [HttpGet("get-all-activitychild")]
        public async Task<ActionResult> GetAllActivityChild()
        {
            ResponseBaseModel result = await _activityChildService.GetAllActivityChild();
            return Ok(result);
        }
        
        [HttpPost("add-update-activitychild")]
        public async Task<ActionResult> AddOrUpdateActivityChild(ActivityChildModel request)
        {
            ResponseBaseModel result = await _activityChildService.AddOrUpdateActivityChild(request);
            return Ok(result);
        }

        [HttpDelete("delete-activitychild/{id}")]
        public async Task<ActionResult> DeleteActivityChild(int id)
        {
            ResponseBaseModel result = await _activityChildService.DeleteActivityChild(id);
            return Ok(result);
        }
    }
}