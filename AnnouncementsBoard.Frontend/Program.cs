using AnnouncementsBoard.Frontend.Application.Services;
using AnnouncementsBoard.Frontend.Application.Services.Interfaces;
using AnnouncementsBoard.Frontend.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IFrontendService, FrontendService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});

builder.Services.AddScoped<IFrontendService, FrontendService>();

builder.Services.AddAutoMapper(typeof(FrontendMappingProfile));
var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
