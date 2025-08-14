using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Task_Progress_Generate_HTTP_Links.Data;
using Task_Progress_Generate_HTTP_Links.Models;

namespace Task_Progress_Generate_HTTP_Links.Controllers
{
    public class RedirectController : Controller
    {
        private readonly UrlDbContext _context;

        public RedirectController(UrlDbContext context)
        {
            _context = context;            
        }

        [Route("{shortUrl}")]
        public async Task<IActionResult> RedirectToOriginal(string shortUrl)
        {
            var url = await _context.Urls
                .FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);

            if (url == null)
                return NotFound();

            // Get the user's IP
            var clientIp = Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString();

            // Fire-and-forget asynchronous visit recording
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
             ?? HttpContext.Connection.RemoteIpAddress?.ToString();

            var visit = new Visit
            {
                UrlId = url.Id,
                IpAddress = ip,
                VisitedAt = DateTime.UtcNow
            };

            await RecordVisitAsync(url.Id);

            // Redirect user to original URL
            return Redirect(url.OriginalUrl);
        }

        private async Task RecordVisitAsync(int urlId)
        {
            // Get the user's IP
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                     ?? HttpContext.Connection.RemoteIpAddress?.ToString();

            var visit = new Visit
            {
                UrlId = urlId,
                IpAddress = ip,
                VisitedAt = DateTime.UtcNow
            };

            // Add a record and save it asynchronously
            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();
        }
    }
}
