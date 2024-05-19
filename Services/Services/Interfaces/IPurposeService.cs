using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IPurposeService : IService
    {
        Task<ResponseBaseModel> GetAllPurpose();
        
        Task<ResponseBaseModel> AddOrUpdatePurpose(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeletePurpose(int id);
    }
}
