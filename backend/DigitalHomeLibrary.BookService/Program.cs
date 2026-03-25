using DigitalHomeLibrary.BookService.Application.Services;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.Services;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models;
using DigitalHomeLibrary.BookService.Infractructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<BookServiceDbContext>(options =>
{
    options.UseNpgsql("User ID=book_db;Password=book_db;Host=postgres;Port=5432;Database=book_db;");
});

builder.Services.AddScoped<IBookRepository, EFCoreBookRepository>();
builder.Services.AddScoped<IAuthorRepository, EFCoreAuthorRepository>();
builder.Services.AddScoped<IReviewRepository, EFCoreReviewRepository>();
builder.Services.AddScoped<IBookTagRepository, EFCoreBookTagRepository>();

builder.Services.AddScoped<BookReviewsService>();
builder.Services.AddScoped<BooksService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<BookTagsService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<BookServiceDbContext>();
context.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
