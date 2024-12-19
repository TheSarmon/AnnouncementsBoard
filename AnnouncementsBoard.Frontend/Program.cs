using AnnouncementsBoard.Frontend.Application.Services;
using AnnouncementsBoard.Frontend.Application.Services.Interfaces;
using AnnouncementsBoard.Frontend.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("AnnouncementsAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7273/api/");
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
