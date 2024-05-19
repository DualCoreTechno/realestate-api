using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BudgetController : BaseController
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet("get-pageload-data")]
        public async Task<ActionResult> GetPageLoadData()
        {
            ResponseBaseModel result = await _budgetService.GetPageLoadData();
            return Ok(result);
        }

        [HttpGet("get-all-budget")]
        public async Task<ActionResult> GetAllBudget()
        {
            ResponseBaseModel result = await _budgetService.GetAllBudget();
            return Ok(result);
        }

        [HttpPost("add-update-budget")]
        public async Task<ActionResult> AddOrUpdateBudget(BudgetModel request)
        {
            ResponseBaseModel result = await _budgetService.AddOrUpdateBudget(request);
            return Ok(result);
        }

        [HttpDelete("delete-budget/{id}")]
        public async Task<ActionResult> DeleteBudget(int id)
        {
            ResponseBaseModel result = await _budgetService.DeleteBudget(id);
            return Ok(result);
        }
    }
}