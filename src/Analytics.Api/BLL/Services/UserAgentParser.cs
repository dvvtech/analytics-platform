namespace Analytics.Api.BLL.Services
{
    public class UserAgentParser
    {        
        public static (string OS, string Browser, string DeviceType) Parse(string userAgent)
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
