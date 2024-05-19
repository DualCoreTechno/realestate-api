using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SegmentController : BaseController
    {
        private readonly ISegmentService _segmentService;

        public SegmentController(ISegmentService segmentService)
        {
            _segmentService = segmentService;
        }

        [HttpGet("get-pageload-data")]
        public async Task<ActionResult> GetPageLoadData()
        {
            ResponseBaseModel result = await _segmentService.GetPageLoadData();
            return Ok(result);
        }

        [HttpGet("get-all-segment")]
        public async Task<ActionResult> GetAllSegment()
        {
            ResponseBaseModel result = await _segmentService.GetAllSegment();
            return Ok(result);
        }

        [HttpPost("add-update-segment")]
        public async Task<ActionResult> AddOrUpdateSegment(SegmentModel request)
        {
            ResponseBaseModel result = await _segmentService.AddOrUpdateSegment(request);
            return Ok(result);
        }

        [HttpDelete("delete-segment/{id}")]
        public async Task<ActionResult> DeleteSegment(int id)
        {
            ResponseBaseModel result = await _segmentService.DeleteSegment(id);
            return Ok(result);
        }
    }
}
