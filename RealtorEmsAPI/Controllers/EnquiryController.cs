using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnquiryController : BaseController
    {
        private readonly IEnquiryService _enquiryService;

        public EnquiryController(IEnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }

        [HttpGet("get-pageload-data")]
        public async Task<ActionResult> GetPageLoadData()
        {
            ResponseBaseModel result = await _enquiryService.GetPageLoadDataAsync(CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }

        [HttpPost("get-all-enquiry")]
        public async Task<ActionResult> GetAllEnquiry(EnquiryRequestModel enquiryRequestModel)
        {
            ResponseBaseModel result = await _enquiryService.GetAllEnquiryAsync(enquiryRequestModel, CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }

        [HttpPost("check-mobile")]
        public async Task<ActionResult> CheckMobileExists(EnquiryCheckMobileModel request)
        {
            ResponseBaseModel result = await _enquiryService.CheckMobileExistsAsync(request);
            return Ok(result);
        }

        [HttpPost("add-update-enquiry")]
        public async Task<ActionResult> AddOrUpdateEnquiry(EnquiryModel request)
        {
            request.AssignBy = CurrentUser.UserId;

            ResponseBaseModel result = await _enquiryService.AddOrUpdateEnquiryAsync(request);
            return Ok(result);
        }

        [HttpPost("add-enquiry-remark")]
        public async Task<ActionResult> AddEnquiryRemark(EnquiryRemarksModel request)
        {
            request.CreatedBy = CurrentUser.UserId;

            ResponseBaseModel result = await _enquiryService.AddEnquiryRemarksAsync(request);
            return Ok(result);
        }

        [HttpGet("get-enquiry-remark/{enquiryId}")]
        public async Task<ActionResult> GetEnquiryRemark(int enquiryId)
        {
            ResponseBaseModel result = await _enquiryService.GetEnquiryRemarksAsync(enquiryId);
            return Ok(result);
        }

        [HttpDelete("delete-enquiry/{id}")]
        public async Task<ActionResult> DeleteEnquiry(int id)
        {
            ResponseBaseModel result = await _enquiryService.DeleteEnquiryAsync(id);
            return Ok(result);
        }

        [HttpPost("send-mail")]
        public ActionResult SendMail(MailModel mailModel)
        {
            ResponseBaseModel result = _enquiryService.SendMail(mailModel);
            return Ok(result);
        }
    }
}
