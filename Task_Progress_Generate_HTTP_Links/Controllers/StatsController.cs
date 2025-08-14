using Microsoft.AspNetCore.Mvc;

using Task_Progress_Generate_HTTP_Links.Data;
using Task_Progress_Generate_HTTP_Links.Models;

namespace Task_Progress_Generate_HTTP_Links.Controllers
{
    public class StatsController : Controller
    {
        private readonly UrlDbContext _context;

        public StatsController(UrlDbContext context)
        {
            _context = context;
        }

        [Route("stats/{secret}")]
        public IActionResult Stats(string secret)
        {
            var url = _context.Urls.FirstOrDefault(u => u.SecretUrl == secret);
            if (url == null)
                return NotFound();

            // Table: Top 10 IPs by latest date
            var visits = _context.Visits
                .OrderByDescending(v => v.VisitedAt)
                .Take(10)
                .Select(v => new VisitStatsViewModel
                {
                    IpAddress = v.IpAddress,
                    Url = _context.Urls.First(u => u.Id == v.UrlId).OriginalUrl,
                    Date = v.VisitedAt
                })
                .ToList();

            // Graph & table: unique visits by day
            var visitList = _context.Visits
                .Where(v => v.UrlId == url.Id)
                .ToList();

            var dailyCounts = visitList
                .GroupBy(v => v.VisitedAt.Date)
                .Select(g => new
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Count = g.Select(v => v.IpAddress).Distinct().Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            ViewBag.DailyCounts = dailyCounts;
            ViewBag.ChartLabels = dailyCounts.Select(x => x.Date).ToList();
            ViewBag.ChartValues = dailyCounts.Select(x => x.Count).ToList();

            return View("~/Views/Stats/Index.cshtml", visits);
        }

        // Method for extracting graph data (JSON format)
        [HttpGet]
        public IActionResult GetChartData(int urlId)
        {
            var chartData = _context.Visits
                .Where(v => v.UrlId == urlId)
                .GroupBy(v => v.VisitedAt.Date)
                .Select(g => new
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Count = g.Select(v => v.IpAddress).Distinct().Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            return Json(chartData);
        }
    }
}
