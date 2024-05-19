using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IRolePermissionService : IService
    {
        Task<ResponseBaseModel> GetRolePermission(int id);
        
        Task<ResponseBaseModel> UpdateRolePermission(RolePermissonResponse request, int userId);
    }
}
