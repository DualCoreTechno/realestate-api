using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IUserService : IService
    {
        Task<ResponseBaseModel> GetPageLoadDataAsync(int userId, int roleId);

        Task<ResponseBaseModel> GetAllUserListAsync(int userId, int roleId);

        Task<ResponseBaseModel> GetUserDetailsWithImageAsync(int userId);

        Task<ResponseBaseModel> GetUserProfileAsync(string userName, int RoleId);

        Task<ResponseBaseModel> CreateUserAsync(UserSignup userSignup);

        Task<ResponseBaseModel> UpdateUserAsync(UserSignup userSignup);

        Task<ResponseBaseModel> UpdateMyProfileAsync(UserSignup userSignup, int userId);

        Task<ResponseBaseModel> DeleteUserAsync(string userId);

        Task<ResponseBaseModel> ChangePasswordAsync(ChangePasswordModel changePassword);

        Task<ResponseBaseModel> ResetPasswordAsync(ChangePasswordModel changePassword);
    }
}
