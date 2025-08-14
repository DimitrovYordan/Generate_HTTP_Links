using Microsoft.EntityFrameworkCore;
using Task_Progress_Generate_HTTP_Links.Data;

namespace Task_Progress_Generate_HTTP_Links
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<UrlDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
