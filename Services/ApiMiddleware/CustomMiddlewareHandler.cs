using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Services.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Services.ApiMiddleware
{
    public class CustomMiddlewareHandler : AuthenticationHandler<CustomMiddlewareOptions>
    {
        public CustomMiddlewareHandler(IOptionsMonitor<CustomMiddlewareOptions> options, ILoggerFactory loggerFactory, UrlEncoder urlEncoder,
            ISystemClock clock) : base(options, loggerFactory, urlEncoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (AuthenticateJWTToken(out var identity, out string message))
            {
                var goAhead = new AuthenticationTicket(new ClaimsPrincipal(identity), Options.Scheme);
                return Task.FromResult(AuthenticateResult.Success(goAhead));
            }

            return Task.FromResult(AuthenticateResult.Fail("Unauthorized, Please check your login credentials"));
        }

        private bool AuthenticateJWTToken(out ClaimsIdentity claimsIdentity, out string message)
        {
            try
            {
                claimsIdentity = null;

                var host = WebUtils.CheckOrigin(Request.HttpContext);

                if (String.IsNullOrEmpty(host))
                {
                    message = "Unauthorized, Your host is not valid";
                    return false;
                }

                Request.Headers.TryGetValue("Authorization", out StringValues headerValue);

                string token = headerValue.ToString().Replace("Bearer ", "").Replace("bearer ", "");

                if (String.IsNullOrEmpty(token))
                {
                    message = "Unauthorized, Token is missing";
                    return false;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    Request.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    message = "Unauthorized, Token is expired";
                    return false;
                }

                claimsIdentity = new ClaimsIdentity(new List<Claim>
                {
                    new (ClaimTypes.Actor, jwtToken?.Payload.ContainsKey("UserId") == true ? jwtToken.Payload["UserId"]?.ToString() : "")
                }, "JWT");

                message = "Success";
                return true;
            }
            catch
            {
                claimsIdentity = new ClaimsIdentity(new List<Claim>
                {
                    new (ClaimTypes.Actor, "")
                }, "JWT");

                message = "Unauthorized, Please check your login credentials";
                return false;
            }
        }
    }
}
