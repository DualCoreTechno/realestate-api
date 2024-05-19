using ViewModels.ViewModels;

namespace Services.Services
{
    public interface ILogDataService : IService
    {
        Task<ResponseBaseModel> AddLogData(LogDataModel request);
    }
}