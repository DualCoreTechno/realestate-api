using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IPropertyService : IService
    {
        Task<ResponseBaseModel> GetPageLoadDataAsync();

        Task<ResponseBaseModel> GetAllPropertyAsync(PropertyRequestModel propertyRequestModel, int userId, int roleId);

        Task<ResponseBaseModel> AddOrUpdatePropertyAsync(PropertyModel request);

        Task<ResponseBaseModel> DeletePropertyAsync(int id);
    }
}
