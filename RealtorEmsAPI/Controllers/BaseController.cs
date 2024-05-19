using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using ViewModels.ViewModels.ResponseModel;

namespace RealtorEmsAPIs.Controllers
{
    public class BaseController : Controller
    {
        protected CurrentUserModel CurrentUser { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            CurrentUser = GetCurrentUser(context.HttpContext.Request);
        }

        public static CurrentUserModel GetCurrentUser(HttpRequest request)
        {
            try
            {
                request.Headers.TryGetValue("Authorization", out StringValues headerValue);

                if (!string.IsNullOrEmpty(headerValue))
                {
                    var handler = new JwtSecurityTokenHandler();

                    var dToken = handler.ReadJwtToken(headerValue.ToString()?.Replace("bearer ", string.Empty).Replace("Bearer ", string.Empty));

                    return new CurrentUserModel
                    {
                        Id = dToken?.Payload.ContainsKey("Id") == true ? dToken.Payload["Id"]?.ToString() : string.Empty,
                        FirstName = dToken?.Payload.ContainsKey("FirstName") == true ? dToken.Payload["FirstName"]?.ToString() : string.Empty,
                        LastName = dToken?.Payload.ContainsKey("LastName") == true ? dToken.Payload["LastName"]?.ToString() : string.Empty,
                        UserId = dToken?.Payload.ContainsKey("UserId") == true ? Convert.ToInt32(dToken.Payload["UserId"]?.ToString()) : 0,
                        UserName = dToken?.Payload.ContainsKey("UserName") == true ? dToken.Payload["UserName"]?.ToString() : string.Empty,
                        Email = dToken?.Payload.ContainsKey("Email") == true ? dToken.Payload["Email"]?.ToString() : string.Empty,
                        PhoneNumber = dToken?.Payload.ContainsKey("PhoneNumber") == true ? dToken.Payload["PhoneNumber"]?.ToString() : string.Empty,
                        ProfileUrl = dToken?.Payload.ContainsKey("ProfileUrl") == true ? dToken.Payload["ProfileUrl"]?.ToString() : string.Empty,
                        RoleId = dToken?.Payload.ContainsKey("RoleId") == true ? Convert.ToInt32(dToken.Payload["RoleId"]) : 0
                    };
                }

                return new CurrentUserModel();
            }
            catch (Exception e)
            {
                return new CurrentUserModel();
            }
        }
    }
}

