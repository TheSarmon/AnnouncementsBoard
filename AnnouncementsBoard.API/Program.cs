using AnnouncementsBoard.Infrastructure.Data;
using AnnouncementsBoard.Infrastructure.Repositories.Interfaces;
using AnnouncementsBoard.Infrastructure.Repositories;
using AnnouncementsBoard.Application.Services.Interfaces;
using AnnouncementsBoard.Application.Services;
using AnnouncementsBoard.Application.Mappings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AnnouncementsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

builder.Services.AddAutoMapper(typeof(AnnouncementMappingProfile));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapControllers();

app.Run();
