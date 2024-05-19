using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SourceController : BaseController
    {
        private readonly ISourceService _sourceService;

        public SourceController(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }

        [HttpGet("get-all-sources")]
        public async Task<ActionResult> GetAllSource()
        {
            ResponseBaseModel result = await _sourceService.GetAllSource();
            return Ok(result);
        }

        [HttpPost("add-update-source")]
        public async Task<ActionResult> AddOrUpdateSource(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _sourceService.AddOrUpdateSource(request);
            return Ok(result);
        }

        [HttpDelete("delete-source/{id}")]
        public async Task<ActionResult> DeleteSource(int id)
        {
            ResponseBaseModel result = await _sourceService.DeleteSource(id);
            return Ok(result);
        }
    }
}
