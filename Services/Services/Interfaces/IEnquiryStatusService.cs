using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IEnquiryStatusService : IService
    {
        Task<ResponseBaseModel> GetAllEnquiryStatus();
        
        Task<ResponseBaseModel> AddOrUpdateEnquiryStatus(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeleteEnquiryStatus(int id);
    }
}
