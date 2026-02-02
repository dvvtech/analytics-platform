
using Analytics.Api.BLL.Abstract;
using Analytics.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Analytics.Api.Controllers
{
    [Route("v1/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private IAnalyticsService _analyticsService;
        private readonly ILogger<AnalyticsController> _logger;        

        public AnalyticsController(
            IAnalyticsService analyticsService,            
            ILogger<AnalyticsController> logger)
        {
            _analyticsService = analyticsService;            
            _logger = logger;
        }

        [HttpPost("track")]
        public async Task<IActionResult> TrackVisit([FromBody] VisitRequest request)
        {            
            var ipAddress = GetClientIpAddress(HttpContext);

            var userAgent = Request.Headers["User-Agent"];

            await _analyticsService.TrackVisitAsync(ipAddress, userAgent, request.Referrer, request.PageUrl);

            return Ok();
        }

        //[HttpGet("stats")]
        //public async Task<IActionResult> GetStats([FromQuery] DateTime? fromDate)
        //{
        //    var query = _context.PageVisits.AsQueryable();

        //    if (fromDate.HasValue)
        //    {
        //        query = query.Where(v => v.VisitTime >= fromDate.Value);
        //    }

        //    var stats = new
        //    {
        //        TotalVisits = await query.CountAsync(),
        //        ByCountry = await query
        //            .GroupBy(v => v.CountryName)
        //            .Select(g => new { Country = g.Key, Count = g.Count() })
        //            .ToListAsync(),
        //        ByOS = await query
        //            .GroupBy(v => v.OperatingSystem)
        //            .Select(g => new { OS = g.Key, Count = g.Count() })
        //            .ToListAsync(),
        //        RecentVisits = await query
        //            .OrderByDescending(v => v.VisitTime)
        //            .Take(50)
        //            .Select(v => new
        //            {
        //                v.CountryName,
        //                v.OperatingSystem,
        //                v.VisitTime,
        //                v.Browser
        //            })
        //            .ToListAsync()
        //    };

        //    return Ok(stats);
        //}

        public static string GetClientIpAddress(HttpContext context)
        {
            if (context == null) return null;

            string ipAddress = null;

            // Приоритетный порядок проверки заголовков
            var headerKeys = new[]
            {
                "X-Forwarded-For",         // Стандартный для прокси
                "X-Real-IP",               // Nginx прокси
                "X-Client-IP",             // Custom
                "CF-Connecting-IP",        // Cloudflare
                //"HTTP_CLIENT_IP",          // Альтернативные названия
                //"HTTP_X_FORWARDED_FOR",
            };

            foreach (var key in headerKeys)
            {
                if (context.Request.Headers.TryGetValue(key, out var value))
                {
                    ipAddress = value.ToString();
                    if (!string.IsNullOrEmpty(ipAddress))
                    {
                        break;
                    }
                }
            }

            // Если заголовки не помогли, используем RemoteIpAddress
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = context.Connection.RemoteIpAddress?.ToString();
            }

            // Очистка IP
            return CleanIpAddress(ipAddress);
        }

        private static string CleanIpAddress(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return ipAddress;

            // Для Swagger/Localhost запросов
            if (ipAddress == "::1" || ipAddress == "127.0.0.1")
            {
                return "localhost";
            }

            // Если это IPv6 localhost
            if (ipAddress.StartsWith("::ffff:127.0.0.1"))
            {
                return "localhost";
            }

            // Убираем порт
            var colonIndex = ipAddress.LastIndexOf(':');
            if (colonIndex > 0)
            {
                // Проверяем IPv6 в квадратных скобках
                if (ipAddress.Contains('[') && ipAddress.Contains(']'))
                {
                    var endBracketIndex = ipAddress.LastIndexOf(']');
                    if (endBracketIndex < colonIndex)
                    {
                        // Формат: [IPv6]:port
                        return ipAddress.Substring(0, endBracketIndex + 1);
                    }
                }
                else
                {
                    var possiblePort = ipAddress.Substring(colonIndex + 1);
                    if (int.TryParse(possiblePort, out _))
                    {
                        // Формат: IPv4:port
                        return ipAddress.Substring(0, colonIndex);
                    }
                }
            }

            // Если несколько IP (цепочка прокси), берем первый
            var ips = ipAddress.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return ips.FirstOrDefault()?.Trim();
        }

        [HttpGet]
        public string Test()
        {
            _logger.LogInformation("call test");

            var ipAddress = GetClientIpAddress(HttpContext);

            _logger.LogInformation($"ip: {ipAddress}");

            return "123";
        }
    }
}
