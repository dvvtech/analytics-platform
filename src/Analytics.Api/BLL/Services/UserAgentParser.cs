namespace Analytics.Api.BLL.Services
{
    public class UserAgentParser
    {
        public static (string OS, string Browser, string DeviceType) Parse(string userAgent)
        {
            var os = "Unknown";
            var browser = "Unknown";
            var device = "Desktop";

            if (userAgent.Contains("Windows NT")) os = "Windows";
            else if (userAgent.Contains("Mac OS X")) os = "macOS";
            else if (userAgent.Contains("Linux")) os = "Linux";
            else if (userAgent.Contains("Android")) os = "Android";
            else if (userAgent.Contains("iPhone") || userAgent.Contains("iPad")) os = "iOS";

            if (userAgent.Contains("Chrome")) browser = "Chrome";
            else if (userAgent.Contains("Firefox")) browser = "Firefox";
            else if (userAgent.Contains("Safari")) browser = "Safari";
            else if (userAgent.Contains("Edge")) browser = "Edge";

            if (userAgent.Contains("Mobile")) device = "Mobile";
            else if (userAgent.Contains("Tablet")) device = "Tablet";

            return (os, browser, device);
        }
    }
}
