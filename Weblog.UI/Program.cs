var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting(); //Identifying action method based route
app.UseAuthentication(); //Reading Identity coockie
app.UseAuthorization();
app.MapControllers(); //Execute the filter pipline (action + filters)
app.Run();
