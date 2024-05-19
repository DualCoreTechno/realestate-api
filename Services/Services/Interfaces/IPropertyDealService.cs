using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IPropertyDealService : IService
    {
        Task<ResponseBaseModel> GetPageLoadDataAsync(int userId, int roleId);

        Task<ResponseBaseModel> GetAllPropertyDealAsync(int userId, int roleId);

        Task<ResponseBaseModel> AddOrUpdatePropertyDealAsync(PropertyDealModel request);

        Task<ResponseBaseModel> DeletePropertyDealAsync(int id);
    }
}
