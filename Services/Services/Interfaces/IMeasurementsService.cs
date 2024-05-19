using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IMeasurementsService : IService
    {
        Task<ResponseBaseModel> GetAllMeasurements();
        
        Task<ResponseBaseModel> AddOrUpdateMeasurement(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeleteMeasurement(int id);
    }
}
