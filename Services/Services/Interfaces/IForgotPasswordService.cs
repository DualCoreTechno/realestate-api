using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IForgotPasswordService : IService
    {
        Task<ResponseBaseModel> SendOTPForForgotPasswordAsync(string emialId);

        Task<ResponseBaseModel> ForgotPasswordAsync(ForgotPasswordModel forgotPassword);
    }
}
