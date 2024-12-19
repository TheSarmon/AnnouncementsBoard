using AnnouncementsBoard.Frontend.Application.Services;
using AnnouncementsBoard.Frontend.Application.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("AnnouncementsAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7273/api/");
});

builder.Services.AddScoped<IFrontendService,FrontendService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
