using DigitalHomeLibrary.BookService.DataAccess.Repositories;
using DigitalHomeLibrary.BookService.DataAccess.Services.Abstract;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models;
using DigitalHomeLibrary.BookService.Infractructure.Repositories;
using DigitalHomeLibrary.BookService.Infractructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BookServiceDbContext>(options =>
{
    options.UseNpgsql("User ID=books_tracker;Password=hnrsygtgr;Host=postgres_container;Port=5432;Database=BooksDB;");
}); 

builder.Services.AddScoped<IBooksRepository, EFCoreBooksRepository>();
builder.Services.AddScoped<ITagsRepository, EFCoreTagsRepository>();

builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IBookTagsService, BooksTagsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
