using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : BaseController
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet("get-all-roles")]
        public async Task<ActionResult> GetAllRoles()
        {
            ResponseBaseModel result = await _rolesService.GetAllRoles();
            return Ok(result);
        }

        [HttpPost("add-update-role")]
        public async Task<ActionResult> AddOrUpdateRole(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _rolesService.AddOrUpdateRole(request);
            return Ok(result);
        }

        [HttpDelete("delete-role/{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            ResponseBaseModel result = await _rolesService.DeleteRole(id);
            return Ok(result);
        }
    }
}
