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

        public static (string OS, string Browser, string DeviceType) Parse2(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return ("Unknown", "Unknown", "Desktop");

            var ua = userAgent.ToLowerInvariant();

            // Определение ОС (проверяем в правильном порядке)
            string os = "Unknown";

            if (ua.Contains("android"))
                os = "Android";
            else if (ua.Contains("iphone") || ua.Contains("ipad") || ua.Contains("ipod"))
                os = "iOS";
            else if (ua.Contains("mac os x"))
                os = "macOS";
            else if (ua.Contains("windows"))
                os = "Windows";
            else if (ua.Contains("linux"))
                os = "Linux";

            // Определение браузера (более точная проверка)
            string browser = "Unknown";

            if (ua.Contains("edg/") || ua.Contains("edge/"))
                browser = "Edge";
            else if (ua.Contains("opr/") || ua.Contains("opera"))
                browser = "Opera";
            else if (ua.Contains("chrome") && !ua.Contains("chromium"))
                browser = "Chrome";
            else if (ua.Contains("firefox") || ua.Contains("fxios"))
                browser = "Firefox";
            else if (ua.Contains("safari") && !ua.Contains("chrome"))
                browser = "Safari";
            else if (ua.Contains("trident") || ua.Contains("msie"))
                browser = "Internet Explorer";

            // Определение типа устройства
            string device = "Desktop";

            if (ua.Contains("mobile") || os == "Android" || os == "iOS")
                device = "Mobile";
            if (ua.Contains("tablet") || (os == "iOS" && ua.Contains("ipad")))
                device = "Tablet";
            if (ua.Contains("smart-tv") || ua.Contains("smarttv"))
                device = "TV";
            if (ua.Contains("bot") || ua.Contains("crawler") || ua.Contains("spider"))
                device = "Bot";

            return (os, browser, device);
        }
    }
}
