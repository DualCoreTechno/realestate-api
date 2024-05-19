using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Services.Settings;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult> LoginUser([FromBody] UserSignin userSignin)
        {
            var requestIp = WebUtils.GetRemoteIp(Request.HttpContext);

            var result = await _authService.UserLoginAsync(userSignin, requestIp);

            if (result == "InActive")
            {
                return Ok(new ResponseBaseModel(500, "User is inactive."));
            }
            else if (result == "RoleInActive")
            {
                return Ok(new ResponseBaseModel(500, "Role is inactive."));
            }
            else if (result != null)
            {
                return Ok(new ResponseBaseModel(200, new { token = result }, "User signin successfully."));
            }
            else
            {
                return Ok(new ResponseBaseModel(401, "Unauthorized, Please check your login credentials"));
            }
        }
    }
}
