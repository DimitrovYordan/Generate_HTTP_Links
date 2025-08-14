using Microsoft.AspNetCore.Mvc;

using Task_Progress_Generate_HTTP_Links.Data;
using Task_Progress_Generate_HTTP_Links.Models;

public class HomeController : Controller
{
    private readonly UrlDbContext _context;
    private readonly Random _rnd = new(); // Used for generating short codes

    public HomeController(UrlDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Shorten(string url)
    {
        // Validate input: not empty, not too long, must start with http:// or https://
        if (string.IsNullOrWhiteSpace(url)
            || url.Length > 2048
            || !(url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                 || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
        {
            // Add a model error if validation fails
            ModelState.AddModelError("url", "Please enter a valid URL (http/https) and no longer than 2048 characters.");
            return View("Index");
        }

        // Generate a short code and secret code
        var shortCode = GenerateShortCode();
        var secretCode = Guid.NewGuid().ToString("N");

        // Save the URL record to the database
        var entity = new Url
        {
            OriginalUrl = url,
            ShortUrl = shortCode,
            SecretUrl = secretCode,
            CreatedAt = DateTime.UtcNow
        };
        _context.Urls.Add(entity);
        _context.SaveChanges();

        // Extract only the domain name for display (without protocol)
        var displayName = GetDisplayName(url);

        // Build clickable URLs for short link and secret stats
        var shortHref = $"{Request.Scheme}://{Request.Host}/{shortCode}";
        var statsHref = $"{Request.Scheme}://{Request.Host}/stats/{secretCode}";

        // Pass values to the View via ViewBag
        ViewBag.DisplayName = displayName;
        ViewBag.ShortHref = shortHref;
        ViewBag.StatsHref = statsHref;

        return View("Index");
    }

    // Helper: Generates a random alphanumeric string for the short code
    private string GenerateShortCode(int length = 6)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_rnd.Next(s.Length)]).ToArray());
    }

    // Helper: Returns only the domain part of a given URL
    private static string GetDisplayName(string url)
    {
        // Try parsing with Uri to safely extract host
        if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return uri.Host;

        // If parsing fails, remove protocol manually
        var trimmed = url.Replace("https://", "", StringComparison.OrdinalIgnoreCase)
                         .Replace("http://", "", StringComparison.OrdinalIgnoreCase);

        // Stop at first '/', '?', or '#' after domain
        var stopIdx = trimmed.IndexOfAny(new[] { '/', '?', '#' });
        return stopIdx >= 0 ? trimmed[..stopIdx] : trimmed;
    }
}
