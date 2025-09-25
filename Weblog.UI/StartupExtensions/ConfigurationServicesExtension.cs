using Microsoft.EntityFrameworkCore;
using Weblog.Core.Domain.RepositoryContracts;
using Weblog.Core.Services;
using Weblog.Infrastructure.DatabaseContext;
using Weblog.Infrastructure.Repositories;

namespace Weblog.UI.StartupExtensions
{
    public static class ConfigurationServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepositroy>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<PostService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<UserService>();

            services.AddDbContext<WeblogDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
