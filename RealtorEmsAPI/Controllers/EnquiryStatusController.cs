using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnquiryStatusController : BaseController
    {
        private readonly IEnquiryStatusService _enquiryStatusService;

        public EnquiryStatusController(IEnquiryStatusService enquiryStatusService)
        {
            _enquiryStatusService = enquiryStatusService;
        }

        [HttpGet("get-all-enquiry-status")]
        public async Task<ActionResult> GetAllEnquiryStatus()
        {
            ResponseBaseModel result = await _enquiryStatusService.GetAllEnquiryStatus();
            return Ok(result);
        }
        
        [HttpPost("add-update-enquiry-status")]
        public async Task<ActionResult> AddOrUpdateEnquiryStatus(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _enquiryStatusService.AddOrUpdateEnquiryStatus(request);
            return Ok(result);
        }

        [HttpDelete("delete-enquiry-status/{id}")]
        public async Task<ActionResult> DeleteEnquiryStatus(int id)
        {
            ResponseBaseModel result = await _enquiryStatusService.DeleteEnquiryStatus(id);
            return Ok(result);
        }
    }
}
