
using Analytics.Api.BLL.Abstract;
using Analytics.Api.BLL.Services;
using Analytics.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Analytics.Api.Controllers
{
    [Route("v1/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly ILogger<AnalyticsController> _logger;
        private readonly IGeoLocationService _geoService;

        public AnalyticsController(
            IGeoLocationService geoService,
            ILogger<AnalyticsController> logger)
        {
            _geoService = geoService;
            _logger = logger;
        }

        [HttpPost("track")]
        public async Task<IActionResult> TrackVisit([FromBody] VisitRequest request)
        {
            var visit = new PageVisit
            {               
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = Request.Headers["User-Agent"].ToString(),
                Referrer = request.Referrer,
                PageUrl = request.PageUrl,
                VisitTime = DateTime.UtcNow
            };

            // Парсим User-Agent
            var (os, browser, device) = UserAgentParser.Parse(visit.UserAgent);
            visit.OperatingSystem = os;
            visit.Browser = browser;
            visit.DeviceType = device;

            visit.IpAddress = "94.19.16.117";
            // Определяем страну
            if (!string.IsNullOrEmpty(visit.IpAddress) && visit.IpAddress != "::1")
            {
                var (countryCode, countryName) = await _geoService.GetCountryFromIp(visit.IpAddress);
                visit.CountryCode = countryCode;
                visit.CountryName = countryName;
            }

            //_context.PageVisits.Add(visit);
            //await _context.SaveChangesAsync();

            return Ok(new { visitId = visit.Id });
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

        [HttpGet]
        public string Test()
        {
            _logger.LogInformation("call test");
            return "123";
        }
    }
}
