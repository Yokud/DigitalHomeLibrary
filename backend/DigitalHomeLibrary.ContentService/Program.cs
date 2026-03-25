using DigitalHomeLibrary.ContentService.Domain.Repositories;
using DigitalHomeLibrary.ContentService.Domain.Services;
using DigitalHomeLibrary.ContentService.Infrastructure.DataAccess;
using DigitalHomeLibrary.ContentService.Infrastructure.Repositories;
using DigitalHomeLibrary.ContentService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ContentServiceDbContext>(options =>
{
    options.UseNpgsql("User ID=content_db;Password=content_db;Host=postgres;Port=5432;Database=content_db;");
});

builder.Services.AddScoped<IBookContentDataRepository, EFCoreBookContentDataRepository>();
builder.Services.AddScoped<IBookContentStorageService, S3BookContentStorageService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ContentServiceDbContext>();
context.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
