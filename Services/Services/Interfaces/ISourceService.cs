using ViewModels.ViewModels;

namespace Services.Services
{
    public interface ISourceService : IService
    {
        Task<ResponseBaseModel> GetAllSource();
        
        Task<ResponseBaseModel> AddOrUpdateSource(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeleteSource(int id);
    }
}
