using Data.Data;
using Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDB _dbContext;
        private readonly SignInManager<Tbl_Users> _signinManager;
        private readonly IConfiguration _configuration;

        public AuthService(AppDB dbContext, SignInManager<Tbl_Users> signInManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _signinManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string?> UserLoginAsync(UserSignin userSigin, string ipAddress)
        {
            try
            {
                Tbl_Users? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == userSigin.PhoneNumber && x.UPassword == userSigin.Password);

                if (user == null)
                    return null;

                if(!user.IsActive)
                    return "InActive";

                var isRoleActive = await _dbContext.Tbl_Role.Where(x => x.Id == user.RoleId).Select(x => x.IsActive).FirstOrDefaultAsync();

                if (!isRoleActive)
                    return "RoleInActive";

                var result = await _signinManager.PasswordSignInAsync(user.UserName, userSigin.Password, false, false);

                if (!result.Succeeded)
                    return null;

                if (result.Succeeded)
                {
                    var authClaims = new List<Claim>
                    {
                        new("Id", user.Id),
                        new("FirstName", user.FirstName),
                        new("LastName", user.LastName),
                        new("UserId", user.UserId.ToString()),
                        new("UserName", user.UserName),
                        new("Email", user.Email),
                        new("PhoneNumber", user.PhoneNumber),
                        new("RoleId", user.RoleId.ToString()),
                        new("TokenGenDate", DateConverter.GetSystemDateTime().ToString()),
                        new(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                       issuer: _configuration["JWT:ValidIssuer"],
                       audience: _configuration["JWT:ValidAudience"],
                       expires: DateConverter.GetSystemDateTime().AddHours(12),
                       claims: authClaims,
                       signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                    );

                    user.LastLoginTime = DateConverter.GetSystemDateTime();
                    user.LastLoginIp = ipAddress;

                    _dbContext.Tbl_Users.Update(user);
                    _dbContext.Entry(user).Property(x => x.UserId).IsModified = false;
                    await _dbContext.SaveChangesAsync();

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                return null;
            }
            catch (Exception e)
            {
                var a = e;
                return null;
            }
        }
    }
}
