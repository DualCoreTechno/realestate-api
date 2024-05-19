using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IActivityService : IService
    {
        Task<ResponseBaseModel> GetAllActivity();

        Task<ResponseBaseModel> GetAllParentActivity();

        Task<ResponseBaseModel> AddOrUpdateActivity(ActivityModel request);

        Task<ResponseBaseModel> DeleteActivity(int id);
    }
}
