using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IActivityParentService : IService
    {
        Task<ResponseBaseModel> GetAllActivityParent();

        Task<ResponseBaseModel> AddOrUpdateActivityParent(MasterCommonFieldsModel request);

        Task<ResponseBaseModel> DeleteActivityParent(int id);
    }
}