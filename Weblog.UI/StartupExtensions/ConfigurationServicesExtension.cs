using Microsoft.EntityFrameworkCore;
using Weblog.Infrastructure.DatabaseContext;

namespace Weblog.UI.StartupExtensions
{
    public static class ConfigurationServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddDbContext<WeblogDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
