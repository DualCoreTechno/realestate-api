using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IDraftReasonService : IService
    {
        Task<ResponseBaseModel> GetAllDraftReasons();
        
        Task<ResponseBaseModel> AddOrUpdateDraftReason(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeleteDraftReason(int id);
    }
}
