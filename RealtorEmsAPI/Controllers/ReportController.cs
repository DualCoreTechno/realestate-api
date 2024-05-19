using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("get-pageload-data")]
        public async Task<ActionResult> GetPageLoadData()
        {
            ResponseBaseModel result = await _reportService.GetPageLoadDataAsync(CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }

        [HttpPost("get-log-report")]
        public async Task<ActionResult> GetLogReport(LogReportRequestModel logReportRequestModel)
        {
            ResponseBaseModel result = await _reportService.GetLogReportAsync(logReportRequestModel, CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }

        [HttpGet("get-login-report")]
        public async Task<ActionResult> GetLoginReport()
        {
            ResponseBaseModel result = await _reportService.GetLoginReportAsync(CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }
    }
}
