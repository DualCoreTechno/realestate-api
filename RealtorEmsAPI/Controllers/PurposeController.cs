using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurposeController : BaseController
    {
        private readonly IPurposeService _purposeService;

        public PurposeController(IPurposeService purposeService)
        {
            _purposeService = purposeService;
        }

        [HttpGet("get-all-purpose")]
        public async Task<ActionResult> GetAllPurpose()
        {
            ResponseBaseModel result = await _purposeService.GetAllPurpose();
            return Ok(result);
        }

        [HttpPost("add-update-purpose")]
        public async Task<ActionResult> AddOrUpdatePurpose(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _purposeService.AddOrUpdatePurpose(request);
            return Ok(result);
        }

        [HttpDelete("delete-purpose/{id}")]
        public async Task<ActionResult> DeletePurpose(int id)
        {
            ResponseBaseModel result = await _purposeService.DeletePurpose(id);
            return Ok(result);
        }
    }
}