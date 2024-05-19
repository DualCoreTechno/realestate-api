using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ViewModels.ViewModels;

namespace RealtorEmsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeasurementsController : BaseController
    {
        private readonly IMeasurementsService _measurementsService;

        public MeasurementsController(IMeasurementsService measurementsService)
        {
            _measurementsService = measurementsService;
        }

        [HttpGet("get-all-measurements")]
        public async Task<ActionResult> GetAllMeasurements()
        {
            ResponseBaseModel result = await _measurementsService.GetAllMeasurements();
            return Ok(result);
        }
        
        [HttpPost("add-update-measurement")]
        public async Task<ActionResult> AddOrUpdateMeasurement(MasterCommonFieldsModel request)
        {
            ResponseBaseModel result = await _measurementsService.AddOrUpdateMeasurement(request);
            return Ok(result);
        }

        [HttpDelete("delete-measurement/{id}")]
        public async Task<ActionResult> DeleteMeasurement(int id)
        {
            ResponseBaseModel result = await _measurementsService.DeleteMeasurement(id);
            return Ok(result);
        }
    }
}
