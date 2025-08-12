using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Task_Progress_Generate_HTTP_Links.Data;
using Task_Progress_Generate_HTTP_Links.Models;
using Task_Progress_Generate_HTTP_Links.Services;

namespace Task_Progress_Generate_HTTP_Links.Controllers
{
    public class UrlController : Controller
    {
        private readonly UrlDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UrlController(UrlDbContext urlDbContext, IServiceProvider serviceProvider)
        {
            this._context = urlDbContext;
            this._serviceProvider = serviceProvider;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/short")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Shorten([FromForm] string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                ModelState.AddModelError("url", "URL can not be empty.");
                return View("Index");
            }

            url = url.Trim();
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                url = url[7..];
            }
            else if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = url[8..];
            }

            var domainPattern = @"^([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(\/.*)?$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(url, domainPattern))
            {
                ModelState.AddModelError("url", "Invalid domain.");
                return View("Index");
            }

            if (!await UrlExistsAsync("https://" + url) && !await UrlExistsAsync("Http://" + url))
            {
                ModelState.AddModelError("url", "The page is not responding or does not exist.");
                return View("Index");
            }

            using var db = _context;

            string shortUrl;
            do
            {
                shortUrl = CodeGenerator.GenerateShortUrl(6);
            }
            while (await db.Urls.AnyAsync(u => u.ShortlUrl == shortUrl));

            string secretUrl;
            do
            {
                secretUrl = CodeGenerator.GenerateSecretUrl(16);
            }
            while (await db.Urls.AnyAsync(u => u.SecretURL == secretUrl));

            var newUrl = new Url
            {
                OriginalUrl = url,
                ShortlUrl = shortUrl,
                SecretURL = secretUrl,
                CreatedAt = DateTime.UtcNow
            };

            var visit = new Visit
            {
                Url = newUrl,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                VisitedAt = DateTime.UtcNow
            };

            db.Urls.Add(newUrl);
            db.Visits.Add(visit);
            await db.SaveChangesAsync();

            var topIds = await _context.Visits
                .GroupBy(v => new { v.IpAddress, v.UrlId })
                .Select(g => new
                {
                    IPAddress = g.Key.IpAddress,
                    Url = _context.Urls.Where(u => u.Id == g.Key.UrlId).Select(u => u.OriginalUrl).FirstOrDefault(),
                    Date = g.Max(v => v.VisitedAt)
                })
                .OrderByDescending(x => x.Date)
                .Take(10)
                .ToListAsync();

            ViewBag.TopIds = topIds;

            var host = Request.Host;
            ViewBag.ShortUrl = $"{host}";
            ViewBag.SecretUrl = $"{host}/stats/{secretUrl}";
            return View("Index");
        }

        [HttpGet("/shorturl")]
        public async Task<IActionResult> RedirectShortUrl(string shortUrl)
        {
            var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortlUrl == shortUrl);
            if (url == null)
            {
                return NotFound();
            }

            var ip = GetClientIp();

            // Asynchronous writing to a separate scope (without blocking redirection)
            _ = Task.Run(async () =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<UrlDbContext>();
                    db.Visits.Add(new Visit
                    {
                        UrlId = url.Id,
                        IpAddress = ip ?? "unknown",
                        VisitedAt = DateTime.UtcNow
                    });

                    await db.SaveChangesAsync();
                }
            });

            return Redirect(url.OriginalUrl);
        }

        [HttpGet("/stats/{secretUrl}")]
        public async Task<IActionResult> Stats(string secretUrl)
        {
            var url = await _context.Urls.Include(u => u.Visits).FirstOrDefaultAsync(u => u.SecretURL == secretUrl);
            if (url == null)
            {
                return NotFound();
            }

            var uniquePerDay = url.Visits
                .GroupBy(v => v.VisitedAt.Date)
                .ToDictionary(g => g.Key, g => g.Select(v => v.IpAddress).Distinct().Count());

            var topIps = url.Visits
                .GroupBy(v => v.IpAddress)
                .Select(g => new TopIdEntry { Ip = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToList();

            var model = new StatsViewModel
            {
                OriginalUrl = url.OriginalUrl,
                UniqueVisitsPerDay = uniquePerDay,
                TopIds = topIps
            };

            return View(model);
        }

        private string GetClientIp()
        {
            var xff = Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(xff))
            {
                var first = xff.Split(',').Select(s => s.Trim()).FirstOrDefault();
                if (!string.IsNullOrEmpty(first))
                {
                    return first;
                }
            }

            return HttpContext.Connection.RemoteIpAddress.ToString();
        }

        private async Task<bool> UrlExistsAsync(string url)
        {
            try
            {
                using(var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(5);
                    var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
