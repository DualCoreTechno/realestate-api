using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using ViewModels.Common;
using ViewModels.Enums;

namespace Services.Settings
{
    public static class WebUtils
    {
        public static string GetRemoteIp(HttpContext context)
        {
            string ip = context.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(ip) || ip == "::1")
            {
                ip = "localhost";
            }

            return ip;
        }

        public static DeviceTypeEnum GetDeviceType(HttpContext context)
        {
            var deviceType = $"User agent : {context.Request.Headers["User-Agent"]}";
            return deviceType.ToLower().Contains("mobi") ? DeviceTypeEnum.MOBILE : DeviceTypeEnum.DESKTOP;
        }

        public static string? GetHost(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue("Host", out StringValues host))
            {
                return null;
            }

            return host;
        }

        public static string? CheckOrigin(HttpContext context)
        {
            var origin = context.Request.Headers["Origin"].ToString();

            bool isOriginValid = AllowedDomainList.DomainArray().Contains(EncryptDecrypt.EncryptText(origin));

            if (isOriginValid)
                return origin;
            else
                return null;
        }
    }
}
