using ViewModels.ViewModels;

namespace Services.Services
{
    public interface INonuseService : IService
    {
        Task<ResponseBaseModel> GetAllNonuse();
        
        Task<ResponseBaseModel> AddOrUpdateNonuse(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeleteNonuse(int id);
    }
}
