using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MinMaxController : BaseController
    {
        private readonly IMinMaxService _minMaxService;

        public MinMaxController(IMinMaxService minMaxService)
        {
            _minMaxService = minMaxService;
        }

        [HttpGet("get-all-min-max")]
        public async Task<ActionResult> GetAllMinMax()
        {
            ResponseBaseModel result = await _minMaxService.GetAllMinMax();
            return Ok(result);
        }

        [HttpPost("add-update-min-max")]
        public async Task<ActionResult> AddOrUpdateMinMax(MinMaxModel request)
        {
            ResponseBaseModel result = await _minMaxService.AddOrUpdateMinMax(request);
            return Ok(result);
        }

        [HttpDelete("delete-min-max/{id}")]
        public async Task<ActionResult> DeleteMinMax(int id)
        {
            ResponseBaseModel result = await _minMaxService.DeleteMinMax(id);
            return Ok(result);
        }
    }
}
