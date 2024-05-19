using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Data.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IHttpContextAccessor _context;

        public UserTokenService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public int GetUserId()
        {
            try
            {
                var claimsIdentity = _context.HttpContext?.User?.Identity as ClaimsIdentity;

                int userId = 0;

                var actor = claimsIdentity?.FindFirst(x => x.Type == ClaimTypes.Actor);

                if(actor != null)
                {
                    userId = Convert.ToInt32(actor.Value);
                }

                return userId;

                //var userId = claimsIdentity?.FindFirst("Actor") != null
                //    ? claimsIdentity.FindFirst("Actor").Value
                //    : string.Empty;

                //if (!string.IsNullOrWhiteSpace(userId))
                //{
                //    return int.Parse(userId);
                //}
                //else
                //{
                //    return 0;
                //}
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}