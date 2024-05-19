using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolePermissionController : BaseController
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet("get-role-permission/{id}")]
        public async Task<ActionResult> GetRolePermission(int id)
        {
            ResponseBaseModel result = await _rolePermissionService.GetRolePermission(id);
            return Ok(result);
        }

        [HttpPost("update-role-permission")]
        public async Task<ActionResult> UpdateRolePermission(RolePermissonResponse request)
        {
            ResponseBaseModel result = await _rolePermissionService.UpdateRolePermission(request, CurrentUser.UserId);
            return Ok(result);
        }
    }
}