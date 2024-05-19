using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IReportService : IService
    {
        Task<ResponseBaseModel> GetPageLoadDataAsync(int userId, int roleId);

        Task<ResponseBaseModel> GetLogReportAsync(LogReportRequestModel logReportRequestModel, int userId, int roleId);

        Task<ResponseBaseModel> GetLoginReportAsync(int userId, int roleId);
    }
}
