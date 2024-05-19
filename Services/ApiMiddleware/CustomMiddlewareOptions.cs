using Microsoft.AspNetCore.Authentication;

namespace Services.ApiMiddleware
{
    public class CustomMiddlewareOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "custome auth";

        public string Scheme => DefaultScheme;

        public bool IsHostOrigin { get; set; }
    }
}
