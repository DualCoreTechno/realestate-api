using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ForgotPasswordController : BaseController
    {
        private readonly IForgotPasswordService _forgotPasswordService;

        public ForgotPasswordController(IForgotPasswordService forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;
        }

        [HttpGet("send-otp/{emailId}")]
        public async Task<ActionResult> SendOTPForForgotPassword(string emailId)
        {
            var result = await _forgotPasswordService.SendOTPForForgotPasswordAsync(emailId);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel forgotPassword)
        {
            var result = await _forgotPasswordService.ForgotPasswordAsync(forgotPassword);
            return Ok(result);
        }
    }
}
