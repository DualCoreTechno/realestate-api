using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IRolesService : IService
    {
        Task<ResponseBaseModel> GetAllRoles();
        
        Task<ResponseBaseModel> AddOrUpdateRole(MasterCommonFieldsModel request);
        
        Task<ResponseBaseModel> DeleteRole(int id);
    }
}
