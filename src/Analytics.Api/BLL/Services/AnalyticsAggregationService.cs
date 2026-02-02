
//using Analytics.Api.DAL;

//namespace Analytics.Api.BLL.Services
//{
//    public class AnalyticsAggregationService
//    {
//        private readonly AnalyticsDbContext _context;
//        private readonly ILogger<AnalyticsAggregationService> _logger;

//        public AnalyticsAggregationService(
//            AnalyticsDbContext context,
//            ILogger<AnalyticsAggregationService> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        public async Task AggregateDailyStatsAsync(DateOnly date)
//        {
//            _logger.LogInformation("Aggregating daily stats for {Date}", date);

//            var startDate = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
//            var endDate = date.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);

//            // Удаляем существующие записи за эту дату (если есть)
//            await _context.DailyStats
//                .Where(s => s.StatDate == date)
//                .ExecuteDeleteAsync();

//            // Агрегируем данные из PageVisits
//            var aggregatedData = await _context.PageVisits
//                .Where(v => v.VisitTime >= startDate && v.VisitTime <= endDate)
//                .GroupBy(v => new
//                {
//                    CountryCode = v.CountryCode ?? "UN",
//                    OperatingSystem = v.OperatingSystem ?? "Unknown"
//                })
//                .Select(g => new DailyStat
//                {
//                    StatDate = date,
//                    CountryCode = g.Key.CountryCode,
//                    OperatingSystem = g.Key.OperatingSystem,
//                    VisitsCount = g.Count()
//                })
//                .ToListAsync();

//            if (aggregatedData.Any())
//            {
//                await _context.DailyStats.AddRangeAsync(aggregatedData);
//                await _context.SaveChangesAsync();
//                _logger.LogInformation("Aggregated {Count} records for {Date}",
//                    aggregatedData.Count, date);
//            }
//            else
//            {
//                _logger.LogInformation("No data to aggregate for {Date}", date);
//            }
//        }

//        public async Task AggregateForTodayAsync()
//        {
//            var today = DateOnly.FromDateTime(DateTime.UtcNow);
//            await AggregateDailyStatsAsync(today);
//        }

//        public async Task AggregateForDateRangeAsync(DateOnly startDate, DateOnly endDate)
//        {
//            for (var date = startDate; date <= endDate; date = date.AddDays(1))
//            {
//                await AggregateDailyStatsAsync(date);
//            }
//        }
//    }
//}
