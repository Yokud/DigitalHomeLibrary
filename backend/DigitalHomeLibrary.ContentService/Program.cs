using DigitalHomeLibrary.ContentService.Application.Services;
using DigitalHomeLibrary.ContentService.Domain.Repositories;
using DigitalHomeLibrary.ContentService.Domain.Services;
using DigitalHomeLibrary.ContentService.Infrastructure.DataAccess;
using DigitalHomeLibrary.ContentService.Infrastructure.Repositories;
using DigitalHomeLibrary.ContentService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<ContentServiceDbContext>(options =>
{
    options.UseNpgsql("User ID=content_user;Password=content_password;Host=postgres;Port=5432;Database=content_db;");
});

builder.Services.AddScoped<IBookContentDataRepository, EFCoreBookContentDataRepository>();
builder.Services.AddScoped<IBookContentStorageService, S3BookContentStorageService>();
builder.Services.AddScoped<BookContentService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ContentServiceDbContext>();
context.Database.EnsureCreated();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Content Service";
        options.Theme = ScalarTheme.Purple;
        options.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
