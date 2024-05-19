using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IAuthService : IService
    {
        Task<string?> UserLoginAsync(UserSignin userSignup, string ipAddress);
    }
}
