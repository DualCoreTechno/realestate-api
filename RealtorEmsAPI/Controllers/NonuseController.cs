using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NonuseController : BaseController
    {
        private readonly INonuseService _nonuseService;

        public NonuseController(INonuseService nonuseService)
        {
            _nonuseService = nonuseService;
        }

        [HttpGet("get-all-nonuse")]
        public async Task<ActionResult> GetAllNonuse()
        {
            ResponseBaseModel result = await _nonuseService.GetAllNonuse();
            return Ok(result);
        }

        [HttpPost("add-update-nonuse")]
        public async Task<ActionResult> AddOrUpdateNonuse(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _nonuseService.AddOrUpdateNonuse(request);
            return Ok(result);
        }

        [HttpDelete("delete-nonuse/{id}")]
        public async Task<ActionResult> DeleteNonuse(int id)
        {
            ResponseBaseModel result = await _nonuseService.DeleteNonuse(id);
            return Ok(result);
        }
    }
}
