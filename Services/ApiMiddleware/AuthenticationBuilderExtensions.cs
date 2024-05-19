using Microsoft.AspNetCore.Authentication;

namespace Services.ApiMiddleware
{
    public static class AuthenticationBuilderExtensions
    {
       public static AuthenticationBuilder AddCustomMiddleware(this AuthenticationBuilder builder, Action<CustomMiddlewareOptions> action) 
       {
            return builder.AddScheme<CustomMiddlewareOptions, CustomMiddlewareHandler>(CustomMiddlewareOptions.DefaultScheme, action);
       }
    }
}
