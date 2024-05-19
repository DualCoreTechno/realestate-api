using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BhkOfficeController : BaseController
    {
        private readonly IBhkOfficeService _bhkOfficeService;

        public BhkOfficeController(IBhkOfficeService bhkOfficeService)
        {
            _bhkOfficeService = bhkOfficeService;
        }

        [HttpGet("get-pageload-data")]
        public async Task<ActionResult> GetPageLoadData()
        {
            ResponseBaseModel result = await _bhkOfficeService.GetPageLoadData();
            return Ok(result);
        }

        [HttpGet("get-all-bhkOffice")]
        public async Task<ActionResult> GetAllBhkOffice()
        {
            ResponseBaseModel result = await _bhkOfficeService.GetAllBhkOffice();
            return Ok(result);
        }

        [HttpPost("add-update-bhkOffice")]
        public async Task<ActionResult> AddOrUpdateBhkOffice(BhkOfficeModel request)
        {
            ResponseBaseModel result = await _bhkOfficeService.AddOrUpdateBhkOffice(request);
            return Ok(result);
        }

        [HttpDelete("delete-bhkOffice/{id}")]
        public async Task<ActionResult> DeleteBhkOffice(int id)
        {
            ResponseBaseModel result = await _bhkOfficeService.DeleteBhkOffice(id);
            return Ok(result);
        }
    }
}
