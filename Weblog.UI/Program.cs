using Microsoft.AspNetCore.Identity;
using Weblog.UI.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder.Configuration);
var app = builder.Build();

async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roleNames = { "Admin", "User" };

    var defaultAdminEmail = "mashmammad876@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(defaultAdminEmail);
    if (adminUser == null)
    {
        var newAdmin = new IdentityUser
        {
            UserName = defaultAdminEmail,
            Email = defaultAdminEmail
        }; 

        var result = await userManager.CreateAsync(newAdmin, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
}


app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); //Identifying action method based route
app.UseAuthentication(); //Reading Identity coockie
app.UseAuthorization();
app.MapControllers(); //Execute the filter pipline (action + filters)
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

    _ = endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller}/{action}/{id?}"
        );
});
app.Run();
