using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Weblog.Core.Domain.IdentityEntities;
using Weblog.Core.Domain.RepositoryContracts;
using Weblog.Core.Services;
using Weblog.Infrastructure.Repositories;
using Weblog.Infrastructure.DatabaseContext;


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
            services.AddScoped<IFileStorageRepository, FileStorageService>();
            services.AddScoped<PostService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<UserService>();
            services.AddScoped<CommentService>();
            services.AddScoped<FileStorageService>();

            services.AddDbContext<WeblogDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            //enable identity in this project
            _ = services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<WeblogDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });
            return services;
        }
    }
}
