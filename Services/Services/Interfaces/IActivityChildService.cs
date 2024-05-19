using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IActivityChildService : IService
    {
        Task<ResponseBaseModel> GetPageLoadData();

        Task<ResponseBaseModel> GetAllActivityChild();

        Task<ResponseBaseModel> AddOrUpdateActivityChild(ActivityChildModel request);

        Task<ResponseBaseModel> DeleteActivityChild(int id);
    }
}