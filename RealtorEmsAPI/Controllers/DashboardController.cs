using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : BaseController
    {
        private readonly IEnquiryService _enquiryService;

        public DashboardController(IEnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }

        [HttpPost("get-enquiry")]
        public async Task<ActionResult> GetAllEnquiry(DashboardRequestModel dashboardRequestModel)
        {
            ResponseBaseModel result = await _enquiryService.GetEnquiryForDashboardAsync(dashboardRequestModel, CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }
    }
}
