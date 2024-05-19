using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IBhkOfficeService : IService
    {
        Task<ResponseBaseModel> GetPageLoadData();

        Task<ResponseBaseModel> GetAllBhkOffice();
        
        Task<ResponseBaseModel> AddOrUpdateBhkOffice(BhkOfficeModel request);
        
        Task<ResponseBaseModel> DeleteBhkOffice(int id);
    }
}
