using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IPropertyStatusService : IService
    {
        Task<ResponseBaseModel> GetAllPropertyStatus();
        
        Task<ResponseBaseModel> AddOrUpdatePropertyStatus(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeletePropertyStatus(int id);
    }
}
