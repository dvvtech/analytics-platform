
using Microsoft.AspNetCore.Mvc;

namespace Analytics.Api.Controllers
{
    [Route("v1/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly ILogger<AnalyticsController> _logger;
        public AnalyticsController(ILogger<AnalyticsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Test()
        {
            _logger.LogInformation("call test");
            return "123";
        }
    }
}
