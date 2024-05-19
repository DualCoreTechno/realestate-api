using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IPropertyTypeService : IService
    {
        Task<ResponseBaseModel> GetAllPropertyType();
        
        Task<ResponseBaseModel> AddOrUpdatePropertyType(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeletePropertyType(int id);
    }
}
