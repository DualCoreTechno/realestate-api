using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IEnquiryService : IService
    {
        Task<ResponseBaseModel> GetPageLoadDataAsync(int userId, int roleId);

        Task<ResponseBaseModel> GetAllEnquiryAsync(EnquiryRequestModel enquiryRequestModel, int userId, int roleId);
        
        Task<ResponseBaseModel> GetEnquiryForDashboardAsync(DashboardRequestModel dashboardRequestModel, int userId, int roleId);

        Task<ResponseBaseModel> CheckMobileExistsAsync(EnquiryCheckMobileModel request);

        Task<ResponseBaseModel> AddOrUpdateEnquiryAsync(EnquiryModel request);

        Task<ResponseBaseModel> AddEnquiryRemarksAsync(EnquiryRemarksModel request);
        
        Task<ResponseBaseModel> GetEnquiryRemarksAsync(int enquiryId);
        
        Task<ResponseBaseModel> DeleteEnquiryAsync(int id);

        ResponseBaseModel SendMail(MailModel mailModel);
    }
}
