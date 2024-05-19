using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-user-profile")]
        public async Task<ActionResult> GetUserProfile()
        {
            ResponseBaseModel result = await _userService.GetUserProfileAsync(CurrentUser.UserName, CurrentUser.RoleId);
            return Ok(result);
        }

        [HttpGet("get-pageload-data")]
        public async Task<ActionResult> GetPageLoadData()
        {
            ResponseBaseModel result = await _userService.GetPageLoadDataAsync(CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }

        [HttpGet("get-all-user-list")]
        public async Task<ActionResult> GetAllUserList()
        {
            ResponseBaseModel result = await _userService.GetAllUserListAsync(CurrentUser.UserId, CurrentUser.RoleId);
            return Ok(result);
        }

        [HttpGet("get-user-details/{userID}")]
        public async Task<ActionResult> GetUserDetailsWithImage(int userID)
        {
            ResponseBaseModel result = await _userService.GetUserDetailsWithImageAsync(userID);
            return Ok(result);
        }

        [HttpGet("get-my-profile")]
        public async Task<ActionResult> GetMyProfile()
        {
            ResponseBaseModel result = await _userService.GetUserDetailsWithImageAsync(CurrentUser.UserId);
            return Ok(result);
        }

        [HttpPut("update-my-profile")]
        public async Task<ActionResult> UpdateMyProfile(UserSignup userSignup)
        {
            ResponseBaseModel result = await _userService.UpdateMyProfileAsync(userSignup, CurrentUser.UserId);
            return Ok(result);
        }

        [HttpPost("add-new-user")]
        public async Task<ActionResult> SignupNewUser(UserSignup userSignup)
        {
            ResponseBaseModel result = await _userService.CreateUserAsync(userSignup);
            return Ok(result);
        }

        [HttpPost("update-user")]
        public async Task<ActionResult> UpdateUser(UserSignup userSignup)
        {
            ResponseBaseModel result = await _userService.UpdateUserAsync(userSignup);
            return Ok(result);
        }

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel changePassword)
        {
            changePassword.UserId = CurrentUser.UserId;

            ResponseBaseModel result = await _userService.ChangePasswordAsync(changePassword);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ChangePasswordModel changePassword)
        {
            ResponseBaseModel result = await _userService.ResetPasswordAsync(changePassword);
            return Ok(result);
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            ResponseBaseModel result = await _userService.DeleteUserAsync(id);
            return Ok(result);
        }
    }
}
