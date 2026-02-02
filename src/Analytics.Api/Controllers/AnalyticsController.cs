
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
            //todo учитывать forward
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var visit = new PageVisit
            {                               
                Referrer = request.Referrer,
                PageUrl = request.PageUrl,
                VisitTime = DateTime.UtcNow
            };

            // Парсим User-Agent
            var (os, browser, device) = UserAgentParser.Parse(Request.Headers["User-Agent"].ToString());
            visit.OperatingSystem = os;
            visit.Browser = browser;
            visit.DeviceType = device;
            
            // Определяем страну
            if (!string.IsNullOrEmpty(ipAddress) && ipAddress != "::1")
            {
                var countryName = await _geoService.GetCountryFromIp(ipAddress);                
                visit.CountryName = countryName;
            }

            //_context.PageVisits.Add(visit);
            //await _context.SaveChangesAsync();

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

        [HttpGet]
        public string Test()
        {
            _logger.LogInformation("call test");
            return "123";
        }
    }
}
