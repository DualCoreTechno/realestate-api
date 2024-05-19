using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DraftReasonController : BaseController
    {
        private readonly IDraftReasonService _draftReasonService;

        public DraftReasonController(IDraftReasonService draftReasonService)
        {
            _draftReasonService = draftReasonService;
        }

        [HttpGet("get-all-draft-reasons")]
        public async Task<ActionResult> GetAllDraftReasons()
        {
            ResponseBaseModel result = await _draftReasonService.GetAllDraftReasons();
            return Ok(result);
        }
        
        [HttpPost("add-update-draft-reason")]
        public async Task<ActionResult> AddOrUpdateDraftReason(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _draftReasonService.AddOrUpdateDraftReason(request);
            return Ok(result);
        }

        [HttpDelete("delete-draft-reason/{id}")]
        public async Task<ActionResult> DeleteDraftReason(int id)
        {
            ResponseBaseModel result = await _draftReasonService.DeleteDraftReason(id);
            return Ok(result);
        }
    }
}
