using Data.Data;
using Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Settings;
using ViewModels.Common;
using ViewModels.ViewModels;

namespace Services.Services
{

    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly AppDB _dbContext;
        private readonly UserManager<Tbl_Users> _userManager;

        public ForgotPasswordService(AppDB dbContext, UserManager<Tbl_Users> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ResponseBaseModel> SendOTPForForgotPasswordAsync(string emialId)
        {
            try
            {
                Tbl_Users? user = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.Email == emialId);

                if (user == null)
                {
                    return new ResponseBaseModel(500, "User not found.");
                }

                string randomOTP = CommonService.GenerateNNumberRandomString(6);

                user.SecurityCode = randomOTP;
                user.OtpSendTime = DateConverter.GetSystemDateTime();

                _dbContext.Tbl_Users.Update(user);
                _dbContext.Entry(user).Property(x => x.UserId).IsModified = false;
                await _dbContext.SaveChangesAsync();

                var subject = "One-Time Password for Forgot Password";
                var mailBody = $"\r\n<?xml version=\"1.0\"?>\r\n<div style=\"font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2\"><div style=\"margin:50px auto;width:70%;padding:20px 0\"><div style=\"border-bottom:1px solid #eee\"><a href=\"\" style=\"font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600\"></a></div><p style=\"font-size:1.1em\">Hi,</p><p>Thank you for choosing us. Use the following OTP to complete your forgot password procedures. OTP is valid for 5 minutes</p><h2 style=\"background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;\">{randomOTP}</h2><hr style=\"border:none;border-top:1px solid #eee\"/>\r\n";

                bool result = MailSender.SendEmail(emialId, subject, mailBody, true, null, null, null, null, null);

                if (result)
                {
                    var userResponse = new SendOtpResponseModel
                    {
                        UserId = user.UserId,
                        UserName = $"{user.FirstName} {user.LastName}"
                    };

                    return new ResponseBaseModel(200, userResponse, "Mail send successfully.");
                }
                else
                {
                    return new ResponseBaseModel(500, "Something went wrong, please try again.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> ForgotPasswordAsync(ForgotPasswordModel forgotPassword)
        {
            try
            {
                Tbl_Users? user = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.UserId == forgotPassword.UserId);

                if (user != null)
                {
                    if (user.OtpSendTime == null)
                    {
                        return new ResponseBaseModel(500, "Otp time not found.");
                    }

                    DateTime otpSendTime = (DateTime)user.OtpSendTime;
                    TimeSpan difference = DateConverter.GetSystemDateTime() - otpSendTime;

                    if (difference.TotalMinutes > 5)
                    {
                        return new ResponseBaseModel(500, "Otp expired, Please resend otp.");
                    }

                    user.UPassword = forgotPassword.Password;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, forgotPassword.Password);
                    user.OtpSendTime = null;
                    user.SecurityCode = string.Empty;

                    _dbContext.Tbl_Users.Update(user);
                    _dbContext.Entry(user).Property(x => x.UserId).IsModified = false;
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Password reset successfully.");
                }
                else
                    return new ResponseBaseModel(404, "User not found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
