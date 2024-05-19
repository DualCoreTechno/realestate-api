using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IPriceService : IService
    {
        Task<ResponseBaseModel> GetAllPrice();
        
        Task<ResponseBaseModel> AddOrUpdatePrice(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeletePrice(int id);
    }
}
