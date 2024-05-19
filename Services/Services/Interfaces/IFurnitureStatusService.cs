using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IFurnitureStatusService : IService
    {
        Task<ResponseBaseModel> GetAllFurnitureStatus();
        
        Task<ResponseBaseModel> AddOrUpdateFurnitureStatus(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeleteFurnitureStatus(int id);
    }
}
