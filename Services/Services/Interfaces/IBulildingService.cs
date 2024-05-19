using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IBuildingService : IService
    {
        Task<ResponseBaseModel> GetAllBuildings();
        
        Task<ResponseBaseModel> AddOrUpdateBuilding(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeleteBuilding(int id);
    }
}
